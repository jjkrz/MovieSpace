using Domain.MoviePersonality;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class MovieRoleConfiguration : IEntityTypeConfiguration<MovieRole>
    {
        public void Configure(EntityTypeBuilder<MovieRole> builder)
        {
            builder.ToTable("MovieRoles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RoleName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
