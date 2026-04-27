using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
{
    public UsuarioRepositorio(AppDbContext context) : base(context) { }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<bool> EmailJaCadastradoAsync(string email)
        => await _dbSet.AnyAsync(u => u.Email == email);
}