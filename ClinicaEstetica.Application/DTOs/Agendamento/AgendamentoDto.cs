namespace ClinicaEstetica.Application.DTOs.Agendamento;

public class AgendamentoDto
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public string NomePaciente { get; set; } = string.Empty;
    public Guid ProfissionalId { get; set; }
    public string NomeProfissional { get; set; } = string.Empty;
    public Guid ProcedimentoId { get; set; }
    public string NomeProcedimento { get; set; } = string.Empty;
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFimPrevista { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
}