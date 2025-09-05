using Domain.Common;
using Domain.Movies;
using Domain.MoviePeople;
using Domain.MoviePersonality;
using Infrastructure.Persistance.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Movie> Movies { get; private set; }
        public DbSet<Genre> Genres { get; private set; }
        public DbSet<ProductionCountry> ProductionCountries { get; private set; }
        public DbSet<MoviePerson> MoviePeople { get; private set; }
        public DbSet<MovieRole> MovieRoles { get; private set; }
        public DbSet<MoviePersonRole> MoviePersonRoles { get; private set; }
        public DbSet<Review> Reviews { get; private set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}
