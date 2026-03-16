using Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.AccessCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasMany(s => s.Participants)
                .WithOne();

            builder.HasIndex(s => s.AccessCode).IsUnique();
        }
    }
}
