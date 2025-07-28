using Domain.MoviePeople;
using Domain.MoviePersonality;
using Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Movies");

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1200);

            builder.Property(m => m.PosterUri)
                .HasConversion(
                    uri => uri == null ? null : uri.ToString(),
                    str => string.IsNullOrEmpty(str) ? null : new Uri(str, UriKind.Absolute))
                .HasMaxLength(500);

            builder.Property(m => m.Duration)
                .HasConversion(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan))
                .IsRequired();

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.HasMany<Genre>("Genres")
                .WithMany("Movies")
                .UsingEntity(j => j.ToTable("MovieGenre"));

            builder.HasMany<ProductionCountry>("ProductionCountries")
                .WithMany("Movies")
                .UsingEntity(j => j.ToTable("MovieCountries"));
        }
    }
}
