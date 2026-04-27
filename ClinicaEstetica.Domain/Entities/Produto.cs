using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Produto : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string? Marca { get; set; }
    public string? Categoria { get; set; }
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal QuantidadeEstoque { get; set; }
    public decimal EstoqueMinimo { get; set; }
    public decimal CustoUnitario { get; set; }
    public decimal? PrecoVenda { get; set; }
    public DateOnly? DataValidade { get; set; }
    public StatusGeral Status { get; set; } = StatusGeral.Ativo;

    // Navegação
    public ICollection<ProcedimentoProduto> Procedimentos { get; set; } = [];
    public ICollection<AtendimentoProduto> Atendimentos { get; set; } = [];
    public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = [];
}