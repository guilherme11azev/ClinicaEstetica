using ClinicaEstetica.Domain.Entities;

namespace ClinicaEstetica.Domain.Interfaces;

public interface IProdutoRepositorio : IRepositorioBase<Produto>
{
    Task<IEnumerable<Produto>> ObterAtivosAsync();
    Task<IEnumerable<Produto>> ObterAbaixoDoEstoqueMinimoAsync();
    Task<Produto?> ObterComMovimentacoesAsync(Guid id);
}