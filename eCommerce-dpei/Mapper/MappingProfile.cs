using AutoMapper;
using eCommerce_dpei.DTOS;
using eCommerce_dpei.Models;

namespace eCommerce_dpei.Mapper
{
    public class MappingProfil:Profile
    {
        public MappingProfil()
        {
            CreateMap<CategoryDto, Category>().
                ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now)).
                ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now)).
                ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<CartDto, Cart>().
               ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.Now)).
               ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
           
            CreateMap<CartUpdateDto, Cart>().
               ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<AddressCreateDto, CustomerAddress>()
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
              .ForMember(dest => dest.CustomerId, opt => opt.Ignore());

            CreateMap<AddressCreateDto, CustomerAddress>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore());

            CreateMap<AddressUpdateDto, CustomerAddress>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    dest.GetType().GetProperty(opts.DestinationMember.Name) != null));

            CreateMap<CustomerAddress, AddressDto>();
            CreateMap<AddressDto, CustomerAddress>();
        }
    }
}
