using Application.ProductionCountries.GetProductionCountries;
using AutoMapper;
using Domain.Movies;

namespace Application.Profiles
{
    public class ProductionCountryProfile : Profile
    {
        public ProductionCountryProfile()
        {
            CreateMap<ProductionCountry, ProductionCountryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}


