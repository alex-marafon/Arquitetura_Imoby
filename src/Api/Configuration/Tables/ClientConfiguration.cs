using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Custom_Architecture.Entities;

namespace Custom_Architecture.Configuration.Tables;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .ToTable("Client");

        builder
            .HasKey(e => e.ClientId)
            .HasName("PK_Client");

        builder
            .Property(e => e.ClientId)
            .ValueGeneratedOnAdd();

        builder
            .Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.CreateDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
    }
}
