using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class AtendimentoRepositorio : RepositorioBase<Atendimento>, IAtendimentoRepositorio
{
    public AtendimentoRepositorio(AppDbContext context) : base(context) { }

    public async Task<Atendimento?> ObterPorAgendamentoAsync(Guid agendamentoId)
        => await _dbSet
            .FirstOrDefaultAsync(a => a.AgendamentoId == agendamentoId);

    public async Task<IEnumerable<Atendimento>> ObterPorPacienteAsync(Guid pacienteId)
        => await _dbSet
            .Include(a => a.Profissional)
            .Include(a => a.Agendamento)
                .ThenInclude(ag => ag.Procedimento)
            .Include(a => a.ProdutosUtilizados)
                .ThenInclude(ap => ap.Produto)
            .Where(a => a.PacienteId == pacienteId)
            .OrderByDescending(a => a.DataHoraInicio)
            .ToListAsync();

    public async Task<Atendimento?> ObterComProdutosAsync(Guid id)
        => await _dbSet
            .Include(a => a.ProdutosUtilizados)
                .ThenInclude(ap => ap.Produto)
            .FirstOrDefaultAsync(a => a.Id == id);
}