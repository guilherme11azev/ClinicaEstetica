using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IProfissionalRepositorio : IRepositorioBase<Profissional>
{
    Task<Profissional?> ObterPorCpfAsync(string cpf);
    Task<IEnumerable<Profissional>> ObterAtivosAsync();
    Task<Profissional?> ObterComProcedimentosAsync(Guid id);
    Task<Profissional?> ObterComDisponibilidadesAsync(Guid id);
}