using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class DisponibilidadeProfissionalMapping : IEntityTypeConfiguration<DisponibilidadeProfissional>
{
    public void Configure(EntityTypeBuilder<DisponibilidadeProfissional> builder)
    {
        builder.ToTable("disponibilidades_profissionais");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DiaSemana)
            .IsRequired();

        builder.Property(d => d.HoraInicio)
            .IsRequired();

        builder.Property(d => d.HoraFim)
            .IsRequired();

        builder.HasOne(d => d.Profissional)
            .WithMany(p => p.Disponibilidades)
            .HasForeignKey(d => d.ProfissionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}