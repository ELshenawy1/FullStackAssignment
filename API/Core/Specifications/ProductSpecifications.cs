using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        public ProductSpecifications(ProductSpecParams productParams) 
            : base(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)))

        {
            ApplyPaging(productParams.PageSize, productParams.PageSize * (productParams.PageIndex - 1));
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
        }
    }
}
