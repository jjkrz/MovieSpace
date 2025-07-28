using Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => new { r.MovieId, r.UserId });

            builder.ToTable("Ratings");

            builder.Property(r => r.Score)
                .IsRequired();

            builder.HasOne(r => r.Movie)
                .WithMany("Ratings");

            builder.HasOne(r => r.User)
                .WithMany("Ratings");
        }
    }
}
