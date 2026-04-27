using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IAtendimentoRepositorio : IRepositorioBase<Atendimento>
{
    Task<Atendimento?> ObterPorAgendamentoAsync(Guid agendamentoId);
    Task<IEnumerable<Atendimento>> ObterPorPacienteAsync(Guid pacienteId);
    Task<Atendimento?> ObterComProdutosAsync(Guid id);
}