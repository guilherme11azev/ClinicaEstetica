using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class PacienteMapping : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("pacientes");

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

        builder.Property(p => p.WhatsApp)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(150);

        builder.Property(p => p.Endereco)
            .HasMaxLength(300);

        builder.Property(p => p.Genero)
            .HasMaxLength(50);

        builder.Property(p => p.Observacoes)
            .HasMaxLength(1000);

        builder.Property(p => p.Status)
            .IsRequired();
    }
}