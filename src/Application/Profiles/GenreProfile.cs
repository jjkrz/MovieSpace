using Application.Genres.GetGenres;
using AutoMapper;
using Domain.Movies;

namespace Application.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
