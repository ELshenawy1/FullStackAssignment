using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Img, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<ProductToAddDTO, Product>()
                .ForMember(d => d.Img, o => o.MapFrom(s => s.Img.FileName));
        }
    }
}
