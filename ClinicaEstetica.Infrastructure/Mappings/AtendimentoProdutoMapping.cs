using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class AtendimentoProdutoMapping : IEntityTypeConfiguration<AtendimentoProduto>
{
    public void Configure(EntityTypeBuilder<AtendimentoProduto> builder)
    {
        builder.ToTable("atendimento_produtos");

        builder.HasKey(ap => new { ap.AtendimentoId, ap.ProdutoId });

        builder.Property(ap => ap.QuantidadeUtilizada)
            .IsRequired()
            .HasColumnType("numeric(10,3)");

        builder.HasOne(ap => ap.Atendimento)
            .WithMany(a => a.ProdutosUtilizados)
            .HasForeignKey(ap => ap.AtendimentoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ap => ap.Produto)
            .WithMany(p => p.Atendimentos)
            .HasForeignKey(ap => ap.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}