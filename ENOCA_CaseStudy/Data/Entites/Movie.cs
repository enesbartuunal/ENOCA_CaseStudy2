namespace ENOCA_CaseStudy.Data.Entites
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }

        public int ProductionYear { get; set; }

        public virtual ICollection<MovieSaloon> MovieSaloons { get; set; }
    }
}
