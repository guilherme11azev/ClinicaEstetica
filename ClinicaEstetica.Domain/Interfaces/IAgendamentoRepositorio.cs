using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IAgendamentoRepositorio : IRepositorioBase<Agendamento>
{
    Task<IEnumerable<Agendamento>> ObterPorProfissionalAsync(Guid profissionalId, DateOnly data);
    Task<IEnumerable<Agendamento>> ObterPorPacienteAsync(Guid pacienteId);
    Task<IEnumerable<Agendamento>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim);
    Task<bool> ExisteConflictoDeHorarioAsync(Guid profissionalId, DateTime inicio, DateTime fim, Guid? agendamentoIdIgnorar = null);
    Task<Agendamento?> ObterComDetalheAsync(Guid id);
}