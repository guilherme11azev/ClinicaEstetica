using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class ProfissionalProcedimentoMapping : IEntityTypeConfiguration<ProfissionalProcedimento>
{
    public void Configure(EntityTypeBuilder<ProfissionalProcedimento> builder)
    {
        builder.ToTable("profissional_procedimentos");

        builder.HasKey(pp => new { pp.ProfissionalId, pp.ProcedimentoId });

        builder.HasOne(pp => pp.Profissional)
            .WithMany(p => p.ProcedimentosHabilitados)
            .HasForeignKey(pp => pp.ProfissionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Procedimento)
            .WithMany(p => p.ProfissionaisHabilitados)
            .HasForeignKey(pp => pp.ProcedimentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}