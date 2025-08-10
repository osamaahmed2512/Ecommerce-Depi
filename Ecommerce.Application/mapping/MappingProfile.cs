using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.domain.entities;


namespace Ecommerce.Application.mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();   
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Updateproductdto, Product>().ReverseMap();
            CreateMap<CartDto, Cart>();
            CreateMap<Cart, UserCartResponseDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<UpdateCartDto, Cart>();
            CreateMap<AddressCreateDto, Address>().ReverseMap();
        }
    }
}
