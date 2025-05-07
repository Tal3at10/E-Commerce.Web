using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecification(int id) : base( p => p.Id == id)
        {
            ApplyInclude();
        }
        public ProductWithBrandsAndTypesSpecification(ProductSpecificationParameters productSpec) : 
            base(
                p => 
                (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId)&&
                (!productSpec.TypeId.HasValue || p.TypeId == productSpec.TypeId)&&
                (string.IsNullOrEmpty(productSpec.SearchByName) || p.Name.ToLower().Contains(productSpec.SearchByName.ToLower()))
                )
        {
            ApplyInclude();
            ApplySorting(productSpec.Sort);
            ApplyPagination(productSpec.PageIndex, productSpec.PageSize);
        }

        private void ApplyInclude()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        private void ApplySorting(string? sort)
        {
            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
    }
}
