using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class ProfissionalMapping : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> builder)
    {
        builder.ToTable("profissionais");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.NomeCompleto)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(14);

        builder.HasIndex(p => p.Cpf)
            .IsUnique();

        builder.Property(p => p.Telefone)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(150);

        builder.Property(p => p.Cargo)
            .HasMaxLength(100);

        builder.Property(p => p.Especialidades)
            .HasMaxLength(500);

        builder.Property(p => p.Status)
            .IsRequired();
    }
}