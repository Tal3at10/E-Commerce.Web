using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // Get All Product

        public Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters productSpec);

        // Get Product By Id

        public Task<ProductResultDto?> GetProductByIdAsync(int id);

        // Get All Brands

        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        // Get All Types

        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
    }
}
