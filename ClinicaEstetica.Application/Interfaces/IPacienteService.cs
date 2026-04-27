using ClinicaEstetica.Application.DTOs.Paciente;

namespace ClinicaEstetica.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> ObterTodosAsync();
    Task<PacienteDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<PacienteDto>> BuscarAsync(string termo);
    Task<PacienteDto> CriarAsync(CriarPacienteDto dto);
    Task AtualizarAsync(Guid id, AtualizarPacienteDto dto);
    Task InativarAsync(Guid id);
}