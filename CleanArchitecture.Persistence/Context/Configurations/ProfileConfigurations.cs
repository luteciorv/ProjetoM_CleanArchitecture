using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Persistence.Context.Configurations
{
    public class ProfileConfigurations : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName");
                name.Property(n => n.LastName).HasColumnName("LastName");
            });

            builder.OwnsOne(p => p.Document, document =>
            {
                document.Property(d => d.Number).HasColumnName("DocumentNumber");
                document.Property(d => d.Type).HasColumnName("DocumentType");
            });
        }
    }
}
