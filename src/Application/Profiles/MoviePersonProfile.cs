using Application.MoviePeople.GetMoviePeople;
using AutoMapper;
using Domain.MoviePeople;

namespace Application.Profiles
{
    public class MoviePersonProfile : Profile
    {
        public MoviePersonProfile()
        {
            CreateMap<MoviePerson, MoviePersonDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
        }
    }
}

