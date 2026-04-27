using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Agendamento : EntidadeBase
{
    public Guid PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; } = null!;

    public Guid ProcedimentoId { get; set; }
    public Procedimento Procedimento { get; set; } = null!;

    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFimPrevista { get; set; }
    public StatusAgendamento Status { get; set; } = StatusAgendamento.Agendado;
    public string? Observacoes { get; set; }
    public string? MotivoCancelamento { get; set; }

    // Navegação
    public Atendimento? Atendimento { get; set; }
}