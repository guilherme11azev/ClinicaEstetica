using ClinicaEstetica.Application.DTOs.Profissional;

namespace ClinicaEstetica.Application.Interfaces;

public interface IProfissionalService
{
    Task<IEnumerable<ProfissionalDto>> ObterTodosAsync();
    Task<IEnumerable<ProfissionalDto>> ObterAtivosAsync();
    Task<ProfissionalDto?> ObterPorIdAsync(Guid id);
    Task<ProfissionalDto> CriarAsync(CriarProfissionalDto dto);
    Task AtualizarAsync(Guid id, AtualizarProfissionalDto dto);
    Task InativarAsync(Guid id);
}