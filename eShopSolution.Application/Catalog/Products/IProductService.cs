using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest productCreateRequest);

        Task<int> Update(ProductUpdateRequest productEditRequest);

        Task<int> Delete(int id);

        Task<ProductViewModel> GetById(int productId, string languageId);

        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest getProductPagingRequest);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> AddViewCount(int productId);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task<int> AddImage(int productId, ProductImageCreateRequest productImageViewModel);

        Task<int> RemoveImage(int productId, int imageId);

        Task<int> UpdateImage(ProductImageUpdateRequest productImageViewModel);
        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<List<ProductImageViewModel>> GetListImage(int productId);
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest getProductPagingRequest);
    }
}