using Domain.MoviePeople;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class MoviePersonConfiguration : IEntityTypeConfiguration<MoviePerson>
    {
        public void Configure(EntityTypeBuilder<MoviePerson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("MoviePeople");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
