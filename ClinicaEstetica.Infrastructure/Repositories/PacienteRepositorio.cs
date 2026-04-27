using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class PacienteRepositorio : RepositorioBase<Paciente>, IPacienteRepositorio
{
    public PacienteRepositorio(AppDbContext context) : base(context) { }

    public async Task<Paciente?> ObterPorCpfAsync(string cpf)
        => await _dbSet.FirstOrDefaultAsync(p => p.Cpf == cpf);

    public async Task<IEnumerable<Paciente>> BuscarAsync(string termo)
        => await _dbSet
            .Where(p => p.NomeCompleto.Contains(termo)
                     || p.Cpf.Contains(termo)
                     || (p.Telefone != null && p.Telefone.Contains(termo))
                     || (p.Email != null && p.Email.Contains(termo)))
            .ToListAsync();

    public async Task<Paciente?> ObterComHistoricoAsync(Guid id)
        => await _dbSet
            .Include(p => p.Agendamentos)
                .ThenInclude(a => a.Procedimento)
            .Include(p => p.Atendimentos)
                .ThenInclude(a => a.ProdutosUtilizados)
                    .ThenInclude(ap => ap.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
}