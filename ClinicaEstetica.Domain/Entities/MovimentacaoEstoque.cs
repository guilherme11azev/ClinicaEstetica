using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class MovimentacaoEstoque : EntidadeBase
{
    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public TipoMovimentacaoEstoque Tipo { get; set; }
    public decimal Quantidade { get; set; }
    public string? Observacoes { get; set; }
    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;
}