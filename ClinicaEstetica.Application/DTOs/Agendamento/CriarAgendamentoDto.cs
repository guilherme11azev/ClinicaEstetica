namespace ClinicaEstetica.Application.DTOs.Agendamento;

public class CriarAgendamentoDto
{
    public Guid PacienteId { get; set; }
    public Guid ProfissionalId { get; set; }
    public Guid ProcedimentoId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public string? Observacoes { get; set; }
}