using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class AtendimentoMapping : IEntityTypeConfiguration<Atendimento>
{
    public void Configure(EntityTypeBuilder<Atendimento> builder)
    {
        builder.ToTable("atendimentos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.DataHoraInicio)
            .IsRequired();

        builder.Property(a => a.Observacoes)
            .HasMaxLength(1000);

        builder.Property(a => a.Status)
            .IsRequired();

        builder.HasOne(a => a.Agendamento)
            .WithOne(a => a.Atendimento)
            .HasForeignKey<Atendimento>(a => a.AgendamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Paciente)
            .WithMany(p => p.Atendimentos)
            .HasForeignKey(a => a.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Profissional)
            .WithMany(p => p.Atendimentos)
            .HasForeignKey(a => a.ProfissionalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}