namespace ClinicaEstetica.Domain.Entities;

public class ProcedimentoProduto
{
    public Guid ProcedimentoId { get; set; }
    public Procedimento Procedimento { get; set; } = null!;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public decimal QuantidadeEstimada { get; set; }
}