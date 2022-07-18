using ENOCA_CaseStudy.Data.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ENOCA_CaseStudy.Data.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Saloon> Saloons { get; set; }
        public DbSet<MovieSaloon> MovieSaloon { get; set; }
    }
}
