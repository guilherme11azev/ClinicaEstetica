using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IPacienteRepositorio : IRepositorioBase<Paciente>
{
    Task<Paciente?> ObterPorCpfAsync(string cpf);
    Task<IEnumerable<Paciente>> BuscarAsync(string termo);
    Task<Paciente?> ObterComHistoricoAsync(Guid id);
}