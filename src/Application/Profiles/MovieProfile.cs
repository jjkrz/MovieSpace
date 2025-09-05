using Application.Movies.GetMovies;
using AutoMapper;
using Domain.Movies;
using Application.MovieRoles.GetMovieRoles;
using Domain.MoviePersonality;
using Application.Common.Dto;
using Application.Genres.GetGenres;
using Application.ProductionCountries.GetProductionCountries;
using System.Xml.Serialization;

namespace Application.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieBriefDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => string.Join(", ", src.Genres.Select(g => g.Name))))
               .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)))
               .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating));
        
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating ?? 0))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.RatingCount))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.ReleaseDate)))
                .ForMember(dest => dest.Directors, opt => opt.MapFrom(src => src.GetDirectors()))
                .ForMember(dest => dest.ScriptWriters, opt => opt.MapFrom(src => src.GetWriters()))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.GetActors()))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.ProductionCountries, opt => opt.MapFrom(src => src.ProductionCountries))
                .ForMember(dest => dest.TopReviews, opt => opt.Ignore());

        }
    }
}
