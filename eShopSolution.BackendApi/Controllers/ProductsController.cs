using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //http://localhost:port/products?pageIndex=1&pageSize=10&categoryId=..
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _productService.GetAllByCategoryId(request);
            return Ok(product);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _productService.GetById(productId, languageId);
            if (product == null)
            {
                return BadRequest("can not find product");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _productService.GetById(productId, request.LanguageId);
            return Created(nameof(GetById), product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _productService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var issuccessFul = await _productService.UpdatePrice(productId, newPrice);
            if (!issuccessFul)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> AddViewCount(int productId)
        {
            var isAdded = await _productService.AddViewCount(productId);
            if (!isAdded)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("{productId}/{quantity}")]
        public async Task<IActionResult> UpdateStock(int productId, int quantity)
        {
            var isUpdateSuccessful = await _productService.UpdateStock(productId, quantity);
            if (!isUpdateSuccessful)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("{productId}/image")]
        public async Task<IActionResult> Create(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
            {
                return BadRequest();
            }
            var image = await _productService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetById), new { id = imageId }, image);
        }

        [HttpGet("{productId}/image/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var product = await _productService.GetImageById(imageId);
            if (product == null)
            {
                return BadRequest("can not find product");
            }
            return Ok(product);
        }

        [HttpPut("{productId}/image/{imageId}")]
        public async Task<IActionResult> UpdateImage([FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _productService.UpdateImage(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("{productId}/image/list")]
        public async Task<IActionResult> GetById(int productId)
        {
            var images = await _productService.GetListImage(productId);
            if (images == null)
            {
                return BadRequest("can not find product");
            }
            return Ok(images);
        }

        [HttpDelete("{productId}/image/{imageId}")]
        public async Task<IActionResult> RemoveImage(int productId, int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isRemoveSuccessful = await _productService.RemoveImage(productId, imageId);
            if (isRemoveSuccessful == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}