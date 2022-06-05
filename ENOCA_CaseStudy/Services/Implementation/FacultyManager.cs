using AutoMapper;
using ENOCA_CaseStudy.Data.Context;
using ENOCA_CaseStudy.Data.Entites;
using ENOCA_CaseStudy.Models;
using ENOCA_CaseStudy.Services.Base;

namespace ENOCA_CaseStudy.Services.Implementation
{
    public class FacultyManager : ServiceAbstractBase<Faculty, FacultyDto>
    {
        public FacultyManager(AppDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}
