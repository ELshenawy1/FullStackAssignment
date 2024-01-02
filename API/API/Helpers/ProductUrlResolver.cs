using AutoMapper;
using AutoMapper.Execution;
using Core.DTOs;
using Core.Entities;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Img))
            {
                return configuration["ApiUrl"] +"Images/"+ source.Img;
            }
            return null;
        }
    }
}
