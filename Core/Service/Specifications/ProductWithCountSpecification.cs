using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecification : BaseSpecifications<Product,int>
    {
        public ProductWithCountSpecification(ProductSpecificationParameters productSpec) :
             base(
                p => 
                (!productSpec.BrandId.HasValue || p.BrandId == productSpec.BrandId)&&
                (!productSpec.TypeId.HasValue || p.TypeId == productSpec.TypeId)&&
                (string.IsNullOrEmpty(productSpec.SearchByName) || p.Name.ToLower().Contains(productSpec.SearchByName.ToLower()))
                )
        {
            
        }
    }
}
