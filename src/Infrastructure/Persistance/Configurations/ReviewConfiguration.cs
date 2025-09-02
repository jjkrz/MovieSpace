using Domain.Movies;
using Infrastructure.Persistance.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => new { r.MovieId, r.UserId });

            builder.ToTable("Reviews");

            builder.HasOne<Movie>()
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId);

            builder.HasOne<ApplicationUser>()
                .WithMany("Reviews")
                .HasForeignKey(r => r.UserId);

            builder.Property(r => r.Content).IsRequired().HasMaxLength(2000);
            
            builder.Property(r => r.Rating).IsRequired();
        }
    }
}
