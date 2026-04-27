using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class ProcedimentoRepositorio : RepositorioBase<Procedimento>, IProcedimentoRepositorio
{
    public ProcedimentoRepositorio(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Procedimento>> ObterAtivosAsync()
        => await _dbSet
            .Where(p => p.Status == StatusGeral.Ativo)
            .ToListAsync();

    public async Task<Procedimento?> ObterComProdutosAsync(Guid id)
        => await _dbSet
            .Include(p => p.ProdutosUtilizados)
                .ThenInclude(pp => pp.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Procedimento?> ObterComProfissionaisAsync(Guid id)
        => await _dbSet
            .Include(p => p.ProfissionaisHabilitados)
                .ThenInclude(pp => pp.Profissional)
            .FirstOrDefaultAsync(p => p.Id == id);
}