using ENOCA_CaseStudy.Models;
using System.Linq.Expressions;

namespace ENOCA_CaseStudy.Services.Abstract
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetBySaloonId(int saloonId);

        Task<string> AddManyToManyRelation(IList<SaloonDto> models, MovieDto movieDto,int releaseYear);

        
    }
}
