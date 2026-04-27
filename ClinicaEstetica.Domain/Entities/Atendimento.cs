using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Atendimento : EntidadeBase
{
    public Guid AgendamentoId { get; set; }
    public Agendamento Agendamento { get; set; } = null!;

    public Guid PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; } = null!;

    public DateTime DataHoraInicio { get; set; }
    public DateTime? DataHoraFim { get; set; }
    public string? Observacoes { get; set; }
    public StatusAtendimento Status { get; set; } = StatusAtendimento.Iniciado;

    // Navegação
    public ICollection<AtendimentoProduto> ProdutosUtilizados { get; set; } = [];
}