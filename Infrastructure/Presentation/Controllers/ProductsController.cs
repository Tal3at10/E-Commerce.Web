using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        // GET BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(Products);
        }


        [HttpGet("{id:int}")]
        //GET BaseUrl/api/Products/10
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }


        [HttpGet("types")]
        //GET BaseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }



        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
