using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class RepositorioBase<T> : IRepositorioBase<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositorioBase(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> ObterPorIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> ObterTodosAsync()
        => await _dbSet.ToListAsync();

    public async Task AdicionarAsync(T entidade)
        => await _dbSet.AddAsync(entidade);

    public Task AtualizarAsync(T entidade)
    {
        _dbSet.Update(entidade);
        return Task.CompletedTask;
    }

    public Task RemoverAsync(T entidade)
    {
        _dbSet.Remove(entidade);
        return Task.CompletedTask;
    }

    public async Task<int> SalvarAsync()
        => await _context.SaveChangesAsync();
}