using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Repositories;

public class AgendamentoRepositorio : RepositorioBase<Agendamento>, IAgendamentoRepositorio
{
    public AgendamentoRepositorio(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Agendamento>> ObterPorProfissionalAsync(Guid profissionalId, DateOnly data)
    {
        var inicio = data.ToDateTime(TimeOnly.MinValue);
        var fim = data.ToDateTime(TimeOnly.MaxValue);

        return await _dbSet
            .Include(a => a.Paciente)
            .Include(a => a.Procedimento)
            .Where(a => a.ProfissionalId == profissionalId
                     && a.DataHoraInicio >= inicio
                     && a.DataHoraInicio <= fim
                     && a.Status != StatusAgendamento.Cancelado)
            .OrderBy(a => a.DataHoraInicio)
            .ToListAsync();
    }

    public async Task<IEnumerable<Agendamento>> ObterPorPacienteAsync(Guid pacienteId)
        => await _dbSet
            .Include(a => a.Profissional)
            .Include(a => a.Procedimento)
            .Where(a => a.PacienteId == pacienteId)
            .OrderByDescending(a => a.DataHoraInicio)
            .ToListAsync();

    public async Task<IEnumerable<Agendamento>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim)
        => await _dbSet
            .Include(a => a.Paciente)
            .Include(a => a.Profissional)
            .Include(a => a.Procedimento)
            .Where(a => a.DataHoraInicio >= inicio && a.DataHoraInicio <= fim)
            .OrderBy(a => a.DataHoraInicio)
            .ToListAsync();

    public async Task<bool> ExisteConflictoDeHorarioAsync(
        Guid profissionalId, DateTime inicio, DateTime fim, Guid? agendamentoIdIgnorar = null)
    {
        return await _dbSet.AnyAsync(a =>
            a.ProfissionalId == profissionalId
            && a.Status != StatusAgendamento.Cancelado
            && (agendamentoIdIgnorar == null || a.Id != agendamentoIdIgnorar)
            && a.DataHoraInicio < fim
            && a.DataHoraFimPrevista > inicio);
    }

    public async Task<Agendamento?> ObterComDetalheAsync(Guid id)
        => await _dbSet
            .Include(a => a.Paciente)
            .Include(a => a.Profissional)
            .Include(a => a.Procedimento)
            .Include(a => a.Atendimento)
            .FirstOrDefaultAsync(a => a.Id == id);
}