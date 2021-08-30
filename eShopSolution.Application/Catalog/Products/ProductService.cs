using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EShopDBContext _eShopDBContext;
        private readonly IStorageService _storageService;
        private const int VIEW_COUNT_INCREASE_VALUE = 1;
        private const int VIEW_COUNT_DEFAULT_VALUE = 0;

        public ProductService(EShopDBContext eShopDbContext, IStorageService storageService)
        {
            _eShopDBContext = eShopDbContext;
            _storageService = storageService;
        }

        public async Task<bool> AddViewCount(int productId)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopSolutionException("Can not find product");
            }
            product.ViewCount += VIEW_COUNT_INCREASE_VALUE;
            return true;
        }

        public async Task<int> Create(ProductCreateRequest productCreateRequest)
        {
            var category = await _eShopDBContext.Categories.FindAsync(productCreateRequest.CategoryId);
            var product = new Product()
            {
                Price = productCreateRequest.Price,
                OriginalPrice = productCreateRequest.OriginalPrice,
                ViewCount = VIEW_COUNT_DEFAULT_VALUE,
                Stock = productCreateRequest.Stock,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = productCreateRequest.Name,
                        Description = productCreateRequest.Description,
                        Details = productCreateRequest.Details,
                        SeoAlias = productCreateRequest.SeoAlias,
                        SeoDescription = productCreateRequest.SeoDescription,
                        SeoTitle = productCreateRequest.SeoTitle,
                        LanguageId = productCreateRequest.LanguageId
                    }
                },
                ProductInCategories = new List<ProductInCategory>()
                {
                    new ProductInCategory()
                    {
                        Category = category,
                        CategoryId = category.Id
                    }
                }
            };
            if (productCreateRequest.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = productCreateRequest.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(productCreateRequest.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _eShopDBContext.Products.Add(product);
            await _eShopDBContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopSolutionException($"Cannot find a product: {productId}");
            }

            var thumbnailImages = _eShopDBContext.ProductImages.Where(i => i.IsDefault && i.ProductId == productId);
            foreach (var image in thumbnailImages)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _eShopDBContext.Products.Remove(product);
            return await _eShopDBContext.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest getProductPagingRequest)
        {
            var query = from product in _eShopDBContext.Products
                        join productTranslation in _eShopDBContext.ProductTranslations on product.Id equals productTranslation.ProductId
                        join productInCategory in _eShopDBContext.ProductInCategorys on product.Id equals productInCategory.ProductId
                        join c in _eShopDBContext.Categories on productInCategory.CategoryId equals c.Id
                        select new { product, productTranslation, productInCategory };

            if (!string.IsNullOrEmpty(getProductPagingRequest.Keyword))
            {
                query = query.Where(x => x.productTranslation.Name.Contains(getProductPagingRequest.Keyword));
            }

            if (getProductPagingRequest.CategoryIds.Count > 0)
            {
                query = query.Where(x => getProductPagingRequest.CategoryIds.Contains(x.productInCategory.CategoryId));
            }

            int totalRecord = query.Count();

            var data = await query.Skip((getProductPagingRequest.PageIndex - 1) * getProductPagingRequest.PageSize)
                            .Take(getProductPagingRequest.PageSize)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.product.Id,
                                Name = x.productTranslation.Name,
                                DateCreated = x.product.DateCreated,
                                Description = x.productTranslation.Description,
                                Details = x.productTranslation.Details,
                                LanguageId = x.productTranslation.LanguageId,
                                OriginalPrice = x.product.OriginalPrice,
                                Price = x.product.Price,
                                SeoAlias = x.productTranslation.SeoAlias,
                                SeoDescription = x.productTranslation.SeoDescription,
                                SeoTitle = x.productTranslation.SeoTitle,
                                Stock = x.product.Stock,
                                ViewCount = x.product.ViewCount
                            }).ToListAsync();

            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRecord,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest productEditRequest)
        {
            var product = await _eShopDBContext.Products.FindAsync(productEditRequest.Id);
            var productTranslations = await _eShopDBContext.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productEditRequest.Id
                                                                                                && x.LanguageId == productEditRequest.LanguageId);
            if (product == null || productTranslations == null)
            {
                throw new EShopSolutionException($"Can not find a product with id: {productEditRequest.Id}");
            }

            productTranslations.Name = productEditRequest.Name;
            productTranslations.SeoAlias = productEditRequest.SeoAlias;
            productTranslations.SeoDescription = productEditRequest.SeoDescription;
            productTranslations.SeoTitle = productEditRequest.SeoTitle;
            productTranslations.Description = productEditRequest.Description;
            productTranslations.Details = productEditRequest.Details;

            if (productEditRequest.ThumbnailImage != null)
            {
                var thumbnailImage = await _eShopDBContext.ProductImages.FirstOrDefaultAsync(i => i.IsDefault && i.ProductId == productEditRequest.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = productEditRequest.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(productEditRequest.ThumbnailImage);
                    _eShopDBContext.ProductImages.Update(thumbnailImage);
                }
            }

            return await _eShopDBContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopSolutionException($"Can not find a product with id: {productId}");
            }

            product.Price = newPrice;
            return await _eShopDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopSolutionException($"Can not find a product with id: {productId}");
            }

            product.Stock += addedQuantity;
            return await _eShopDBContext.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            var productTranslation = await _eShopDBContext.ProductTranslations
                                                    .FirstOrDefaultAsync(x => x.ProductId == productId && languageId == x.LanguageId);
            if (product == null)
            {
                throw new EShopSolutionException($"Can not find product with id: {productId}");
            }

            var productViewModel = new ProductViewModel()
            {
                DateCreated = product.DateCreated,
                Id = product.Id,
                Description = productTranslation != null ? productTranslation.Description : null,
                Details = productTranslation != null ? productTranslation.Details : null,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                LanguageId = languageId,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _eShopDBContext.ProductImages.Add(productImage);
            await _eShopDBContext.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage(int productId, int imageId)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            var productImage = await _eShopDBContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopSolutionException("Can not find image");
            }
            if (product == null)
            {
                throw new EShopSolutionException("Can not find product");
            }
            product.ProductImages.Remove(productImage);
            _eShopDBContext.ProductImages.Remove(productImage);
            return await _eShopDBContext.SaveChangesAsync();

        }

        public async Task<int> UpdateImage(ProductImageUpdateRequest request)
        {
            var productImage = await _eShopDBContext.ProductImages.FindAsync(request.Id);
            if (productImage == null)
            {
                throw new EShopSolutionException($"Can not find an image with id: {request.Id}");
            }

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _eShopDBContext.ProductImages.Update(productImage);
            return await _eShopDBContext.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var product = await _eShopDBContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopSolutionException("Can not find product");
            }
            var productImageViewModels = new ConcurrentBag<ProductImageViewModel>();
            Parallel.ForEach(product.ProductImages, image =>
            {
                var productImageViewModel = new ProductImageViewModel();
                productImageViewModel.Caption = image.Caption;
                productImageViewModel.DateCreated = image.DateCreated;
                productImageViewModel.ImagePath = image.ImagePath;
                productImageViewModel.IsDefault = image.IsDefault;
                productImageViewModel.SortOrder = image.SortOrder;
                productImageViewModel.ProductId = image.ProductId;
                productImageViewModel.FileSize = image.FileSize;

                productImageViewModels.Add(productImageViewModel);
            });

            return productImageViewModels.ToList();
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var productImage = await _eShopDBContext.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new EShopSolutionException($"Can not find image with id: {imageId}");
            }

            return new ProductImageViewModel()
            {
                Caption = productImage.Caption,
                DateCreated = productImage.DateCreated,
                ImagePath = productImage.ImagePath,
                IsDefault = productImage.IsDefault,
                SortOrder = productImage.SortOrder,
                ProductId = productImage.ProductId,
                FileSize = productImage.FileSize
            };
        }
        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest getProductPagingRequest)
        {
            var query = from product in _eShopDBContext.Products
                        join productTranslation in _eShopDBContext.ProductTranslations on product.Id equals productTranslation.Id
                        join productInCategory in _eShopDBContext.ProductInCategorys on product.Id equals productInCategory.ProductId
                        join category in _eShopDBContext.Categories on productInCategory.CategoryId equals category.Id
                        where productTranslation.LanguageId == getProductPagingRequest.LanguageId
                        select new { product, productTranslation, productInCategory };

            if (getProductPagingRequest.CategoryId.HasValue && getProductPagingRequest.CategoryId.Value > 0)
            {
                query = query.Where(x => x.productInCategory.CategoryId == getProductPagingRequest.CategoryId.Value);
            }

            var totalRow = await query.CountAsync();

            var data = await query.Skip((getProductPagingRequest.PageIndex - 1) * getProductPagingRequest.PageSize)
                                  .Take(getProductPagingRequest.PageSize)
                                  .Select(x => new ProductViewModel()
                                  {
                                      Id = x.product.Id,
                                      DateCreated = x.product.DateCreated,
                                      Description = x.productTranslation.Description,
                                      Details = x.productTranslation.Details,
                                      LanguageId = x.productTranslation.LanguageId,
                                      Name = x.productTranslation.Name,
                                      OriginalPrice = x.product.OriginalPrice,
                                      Price = x.product.Price,
                                      SeoAlias = x.productTranslation.SeoAlias,
                                      SeoDescription = x.productTranslation.SeoDescription,
                                      SeoTitle = x.productTranslation.SeoTitle,
                                      Stock = x.product.Stock,
                                      ViewCount = x.product.ViewCount
                                  }).ToListAsync();

            var pageResult = new PageResult<ProductViewModel>()
            {
                Items = data,
                TotalRecord = totalRow
            };
            return pageResult;
        }
    }
}