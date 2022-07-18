using AutoMapper;
using ENOCA_CaseStudy.Data.Entites;
using ENOCA_CaseStudy.Models;

namespace ENOCA_CaseStudy.Services.Mapper
{
    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            CreateMap<Movie, MovieDto>().ForMember(x => x.SaloonNames, y => y.MapFrom(z => z.MovieSaloons.Select(x => x.Saloon.SaloonName)));
            CreateMap<MovieDto, Movie>();
            CreateMap<Saloon, SaloonDto>();
            CreateMap<SaloonDto, Saloon>();
        }
    }
}
