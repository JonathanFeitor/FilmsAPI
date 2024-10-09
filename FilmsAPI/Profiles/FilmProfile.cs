using AutoMapper;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;

namespace FilmsAPI.Profiles;

public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<CreateFilmDto, Film>();
        CreateMap<UpdateFilmDto, Film>();
        CreateMap<Film, UpdateFilmDto>(); 
        CreateMap<Film, ReadFilmDto>();
    }
}
