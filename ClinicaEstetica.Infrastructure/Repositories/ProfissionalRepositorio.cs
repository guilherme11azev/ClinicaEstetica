using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class ProfissionalRepositorio : RepositorioBase<Profissional>, IProfissionalRepositorio
{
    public ProfissionalRepositorio(AppDbContext context) : base(context) { }

    public async Task<Profissional?> ObterPorCpfAsync(string cpf)
        => await _dbSet.FirstOrDefaultAsync(p => p.Cpf == cpf);

    public async Task<IEnumerable<Profissional>> ObterAtivosAsync()
        => await _dbSet
            .Where(p => p.Status == StatusGeral.Ativo)
            .ToListAsync();

    public async Task<Profissional?> ObterComProcedimentosAsync(Guid id)
        => await _dbSet
            .Include(p => p.ProcedimentosHabilitados)
                .ThenInclude(pp => pp.Procedimento)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Profissional?> ObterComDisponibilidadesAsync(Guid id)
        => await _dbSet
            .Include(p => p.Disponibilidades)
            .FirstOrDefaultAsync(p => p.Id == id);
}