using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters productSpec)
        {
            //var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(false);
            var spec = new ProductWithBrandsAndTypesSpecification(productSpec);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            var count = await unitOfWork.GetRepository<Product,int>().GetCountAsync(new ProductWithCountSpecification(productSpec));

            return new PaginationResponse<ProductResultDto>(productSpec.PageIndex,productSpec.PageSize,count,result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
            if (product is null) throw new ProductNotFound(id); 
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(false);
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(false);
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

        
    }
}
