using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class ProcedimentoProdutoMapping : IEntityTypeConfiguration<ProcedimentoProduto>
{
    public void Configure(EntityTypeBuilder<ProcedimentoProduto> builder)
    {
        builder.ToTable("procedimento_produtos");

        builder.HasKey(pp => new { pp.ProcedimentoId, pp.ProdutoId });

        builder.Property(pp => pp.QuantidadeEstimada)
            .IsRequired()
            .HasColumnType("numeric(10,3)");

        builder.HasOne(pp => pp.Procedimento)
            .WithMany(p => p.ProdutosUtilizados)
            .HasForeignKey(pp => pp.ProcedimentoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Produto)
            .WithMany(p => p.Procedimentos)
            .HasForeignKey(pp => pp.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}