using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Procedimento : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public CategoriaProcedimento Categoria { get; set; }
    public int DuracaoEstimadaMinutos { get; set; }
    public decimal PrecoBase { get; set; }
    public string? Requisitos { get; set; }
    public StatusGeral Status { get; set; } = StatusGeral.Ativo;

    // Navegação
    public ICollection<ProfissionalProcedimento> ProfissionaisHabilitados { get; set; } = [];
    public ICollection<ProcedimentoProduto> ProdutosUtilizados { get; set; } = [];
    public ICollection<Agendamento> Agendamentos { get; set; } = [];
}