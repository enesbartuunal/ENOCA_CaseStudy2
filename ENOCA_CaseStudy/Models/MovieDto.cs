using ENOCA_CaseStudy.Data.Entites;

namespace ENOCA_CaseStudy.Models
{
    public class MovieDto
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }

        public int ProductionYear { get; set; }

        public IList<string> SaloonNames { get; set; }

  
    }
}
