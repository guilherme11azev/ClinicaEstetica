using ClinicaEstetica.Application.DTOs.Agendamento;

namespace ClinicaEstetica.Application.Interfaces;

public interface IAgendamentoService
{
    Task<IEnumerable<AgendamentoDto>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim);
    Task<IEnumerable<AgendamentoDto>> ObterPorProfissionalAsync(Guid profissionalId, DateOnly data);
    Task<IEnumerable<AgendamentoDto>> ObterPorPacienteAsync(Guid pacienteId);
    Task<AgendamentoDto?> ObterPorIdAsync(Guid id);
    Task<AgendamentoDto> CriarAsync(CriarAgendamentoDto dto);
    Task CancelarAsync(Guid id, CancelarAgendamentoDto dto);
    Task AtualizarStatusAsync(Guid id, string novoStatus);
}