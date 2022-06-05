using AutoMapper;
using ENOCA_CaseStudy.Data.Entites;
using ENOCA_CaseStudy.Models;

namespace ENOCA_CaseStudy.Services.Mapper
{
    public class MappinProfile : Profile
    {
        public MappinProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<Faculty, FacultyDto>();
            CreateMap<FacultyDto, Faculty>();
            CreateMap<Section, SectionDto>();
            CreateMap<SectionDto, SectionDto>();
        }
    }
}
