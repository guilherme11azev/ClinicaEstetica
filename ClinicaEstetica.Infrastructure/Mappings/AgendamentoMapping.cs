using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class AgendamentoMapping : IEntityTypeConfiguration<Agendamento>
{
    public void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        builder.ToTable("agendamentos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.DataHoraInicio)
            .IsRequired();

        builder.Property(a => a.DataHoraFimPrevista)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired();

        builder.Property(a => a.Observacoes)
            .HasMaxLength(500);

        builder.Property(a => a.MotivoCancelamento)
            .HasMaxLength(500);

        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Profissional)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.ProfissionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Procedimento)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.ProcedimentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}