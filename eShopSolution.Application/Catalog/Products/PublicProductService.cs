using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDBContext _eShopDbContext;

        public PublicProductService(EShopDBContext eShopDbContext)
        {
            _eShopDbContext = eShopDbContext;
        }

        public async Task<PageResult<ProductViewModel>> GetAll(string languageId)
        {
            var query = from product in _eShopDbContext.Products
                        join productTranslation in _eShopDbContext.ProductTranslations on product.Id equals productTranslation.Id
                        join productInCategory in _eShopDbContext.ProductInCategorys on product.Id equals productInCategory.ProductId
                        join category in _eShopDbContext.Categories on productInCategory.CategoryId equals category.Id
                        where productTranslation.LanguageId == languageId
                        select new { product, productTranslation, productInCategory };

            var totalRow = await query.CountAsync();

            var data = await query.Select(x => new ProductViewModel()
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

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest getProductPagingRequest)
        {
            var query = from product in _eShopDbContext.Products
                        join productTranslation in _eShopDbContext.ProductTranslations on product.Id equals productTranslation.Id
                        join productInCategory in _eShopDbContext.ProductInCategorys on product.Id equals productInCategory.ProductId
                        join category in _eShopDbContext.Categories on productInCategory.CategoryId equals category.Id
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