using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaEstetica.Infrastructure.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Marca)
            .HasMaxLength(100);

        builder.Property(p => p.Categoria)
            .HasMaxLength(100);

        builder.Property(p => p.UnidadeMedida)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(p => p.QuantidadeEstoque)
            .IsRequired()
            .HasColumnType("numeric(10,3)");

        builder.Property(p => p.EstoqueMinimo)
            .IsRequired()
            .HasColumnType("numeric(10,3)");

        builder.Property(p => p.CustoUnitario)
            .IsRequired()
            .HasColumnType("numeric(10,2)");

        builder.Property(p => p.PrecoVenda)
            .HasColumnType("numeric(10,2)");

        builder.Property(p => p.Status)
            .IsRequired();
    }
}