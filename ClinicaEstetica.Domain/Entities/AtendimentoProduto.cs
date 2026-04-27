namespace ClinicaEstetica.Domain.Entities;

public class AtendimentoProduto
{
    public Guid AtendimentoId { get; set; }
    public Atendimento Atendimento { get; set; } = null!;

    public Guid ProdutoId { get; set; }
    public Produto Produto { get; set; } = null!;

    public decimal QuantidadeUtilizada { get; set; }
}