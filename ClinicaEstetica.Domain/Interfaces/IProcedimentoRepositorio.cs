using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IProcedimentoRepositorio : IRepositorioBase<Procedimento>
{
    Task<IEnumerable<Procedimento>> ObterAtivosAsync();
    Task<Procedimento?> ObterComProdutosAsync(Guid id);
    Task<Procedimento?> ObterComProfissionaisAsync(Guid id);
}