using Domain.MoviePersonality;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class MoviePersonRoleConfiguration : IEntityTypeConfiguration<MoviePersonRole>
    {
        public void Configure(EntityTypeBuilder<MoviePersonRole> builder)
        {
            builder.ToTable("MoviePersonRoles");

            builder.HasKey(x => new { x.MovieId, x.MoviePersonId, x.MovieRoleId });

            builder.HasOne(x => x.Movie)
               .WithMany(x => x.MoviePeople)
               .HasForeignKey(x => x.MovieId);

            builder.HasOne(x => x.MoviePerson)
                .WithMany("Roles")
                .HasForeignKey(x => x.MoviePersonId);

            builder.HasOne(x => x.MovieRole)
                .WithMany()
                .HasForeignKey(x => x.MovieRoleId);

            builder.Property(x => x.CharacterName)
                .HasMaxLength(100);
        }
    }
}
