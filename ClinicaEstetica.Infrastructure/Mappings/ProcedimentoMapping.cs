using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class ProcedimentoMapping : IEntityTypeConfiguration<Procedimento>
{
    public void Configure(EntityTypeBuilder<Procedimento> builder)
    {
        builder.ToTable("procedimentos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Descricao)
            .HasMaxLength(500);

        builder.Property(p => p.Categoria)
            .IsRequired();

        builder.Property(p => p.DuracaoEstimadaMinutos)
            .IsRequired();

        builder.Property(p => p.PrecoBase)
            .IsRequired()
            .HasColumnType("numeric(10,2)");

        builder.Property(p => p.Requisitos)
            .HasMaxLength(500);

        builder.Property(p => p.Status)
            .IsRequired();
    }
}