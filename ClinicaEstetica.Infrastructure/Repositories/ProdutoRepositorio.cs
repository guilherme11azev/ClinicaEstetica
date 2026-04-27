using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
{
    public ProdutoRepositorio(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Produto>> ObterAtivosAsync()
        => await _dbSet
            .Where(p => p.Status == StatusGeral.Ativo)
            .ToListAsync();

    public async Task<IEnumerable<Produto>> ObterAbaixoDoEstoqueMinimoAsync()
        => await _dbSet
            .Where(p => p.QuantidadeEstoque <= p.EstoqueMinimo
                     && p.Status == StatusGeral.Ativo)
            .ToListAsync();

    public async Task<Produto?> ObterComMovimentacoesAsync(Guid id)
        => await _dbSet
            .Include(p => p.Movimentacoes)
            .FirstOrDefaultAsync(p => p.Id == id);
}