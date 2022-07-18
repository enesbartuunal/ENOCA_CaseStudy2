using AutoMapper;
using ENOCA_CaseStudy.Data.Context;
using ENOCA_CaseStudy.Data.Entites;
using ENOCA_CaseStudy.Models;
using ENOCA_CaseStudy.Services.Abstract;
using ENOCA_CaseStudy.Services.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ENOCA_CaseStudy.Services.Implementation
{
    public class MovieManager : ServiceAbstractBase<Movie, MovieDto>, IMovieService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public MovieManager(AppDbContext db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShowReleaseHistoryDto>> GetReleaseHistory(int movieId)
        {
            var list = _db.MovieSaloon.Where(x => x.MovieId == movieId).ToList();
            List<ShowReleaseHistoryDto> resultList = new List<ShowReleaseHistoryDto>();
            if (list.Count > 0)
            {
                foreach (var movieSaloon in list)
                {
                    var item = new ShowReleaseHistoryDto()
                    {
                        SaloonName = _db.Saloons.Where(x => x.SaloonID == movieSaloon.SaloonId).Select(x => x.SaloonName).First(),
                        MovieName = _db.Movies.Where(x => x.MovieID == movieSaloon.MovieId).Select(x => x.MovieName).First(),
                        ReleaseYear = movieSaloon.ReleaseYear.ToString(),
                    };
                    resultList.Add(item);
                }
                return resultList;
            }
          
            return resultList;
        }

        public async Task<string> AddManyToManyRelation(IList<SaloonDto> models, MovieDto movieDto, int releaseYear)
        {
            try
            {
                var movie = await _db.Set<Movie>().FindAsync(movieDto.MovieID);
                foreach (SaloonDto saloon in models)
                {
                    var saloonEntity = _mapper.Map<Saloon>(saloon);
                    var relation = new MovieSaloon()
                    {
                        Movie = movie,
                        SaloonId = saloon.SaloonID,
                        ReleaseYear = releaseYear
                    };
                    _db.MovieSaloon.Add(relation);
                }
                await _db.SaveChangesAsync();
                return "İlişki Kuruldu.";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<MovieDto>> GetBySaloonId(int saloonId)
        {
            var list = _db.MovieSaloon.Where(x => x.SaloonId == saloonId).Select(y => y.Movie);
            var modellistquery = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDto>>(list);
            return modellistquery.ToList();
        }



    }
}
