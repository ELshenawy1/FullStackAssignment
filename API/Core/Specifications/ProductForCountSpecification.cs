using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductForCountSpecification : BaseSpecification<Product>
    {
        public ProductForCountSpecification(ProductSpecParams productParams)
            : base(p =>
            ( string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search) ))
        {
        }
    }
}
