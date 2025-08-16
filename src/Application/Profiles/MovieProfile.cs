using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Movies.GetMovies;
using AutoMapper;
using Domain.Movies;

namespace Application.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieBriefDto>()
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => string.Join(", ", src.Genres.Select(g => g.Name))))
               .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)))
               .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.GetAverageRating()));
        }
    }
}
