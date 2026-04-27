using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class MovimentacaoEstoqueMapping : IEntityTypeConfiguration<MovimentacaoEstoque>
{
    public void Configure(EntityTypeBuilder<MovimentacaoEstoque> builder)
    {
        builder.ToTable("movimentacoes_estoque");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tipo)
            .IsRequired();

        builder.Property(m => m.Quantidade)
            .IsRequired()
            .HasColumnType("numeric(10,3)");

        builder.Property(m => m.Observacoes)
            .HasMaxLength(500);

        builder.Property(m => m.DataMovimentacao)
            .IsRequired();

        builder.HasOne(m => m.Produto)
            .WithMany(p => p.Movimentacoes)
            .HasForeignKey(m => m.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}