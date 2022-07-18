namespace ENOCA_CaseStudy.Data.Entites
{
    public class MovieSaloon
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int SaloonId { get; set; }
        public Saloon Saloon { get; set; }
        public int ReleaseYear { get; set; }
    }
}
