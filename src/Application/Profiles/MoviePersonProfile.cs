using Application.Common.Dto;
using Application.MoviePeople.GetMoviePeople;
using Application.MovieRoles.GetMovieRoles;
using AutoMapper;
using Domain.MoviePeople;
using Domain.MoviePersonality;

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

            CreateMap<MovieRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName));

            CreateMap<MoviePerson, DirectorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

            CreateMap<MoviePerson, ActorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

            CreateMap<MoviePerson, ScriptWriterDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));
        }
    }
}

