using ClinicaEstetica.Application.DTOs.Agendamento;
using ClinicaEstetica.Application.Interfaces;
using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Domain.Interfaces;

namespace ClinicaEstetica.Application.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepositorio _agendamentoRepositorio;
    private readonly IPacienteRepositorio _pacienteRepositorio;
    private readonly IProfissionalRepositorio _profissionalRepositorio;
    private readonly IProcedimentoRepositorio _procedimentoRepositorio;

    public AgendamentoService(
        IAgendamentoRepositorio agendamentoRepositorio,
        IPacienteRepositorio pacienteRepositorio,
        IProfissionalRepositorio profissionalRepositorio,
        IProcedimentoRepositorio procedimentoRepositorio)
    {
        _agendamentoRepositorio = agendamentoRepositorio;
        _pacienteRepositorio = pacienteRepositorio;
        _profissionalRepositorio = profissionalRepositorio;
        _procedimentoRepositorio = procedimentoRepositorio;
    }

    public async Task<IEnumerable<AgendamentoDto>> ObterPorPeriodoAsync(DateTime inicio, DateTime fim)
    {
        var agendamentos = await _agendamentoRepositorio.ObterPorPeriodoAsync(inicio, fim);
        return agendamentos.Select(MapearParaDto);
    }

    public async Task<IEnumerable<AgendamentoDto>> ObterPorProfissionalAsync(Guid profissionalId, DateOnly data)
    {
        var agendamentos = await _agendamentoRepositorio.ObterPorProfissionalAsync(profissionalId, data);
        return agendamentos.Select(MapearParaDto);
    }

    public async Task<IEnumerable<AgendamentoDto>> ObterPorPacienteAsync(Guid pacienteId)
    {
        var agendamentos = await _agendamentoRepositorio.ObterPorPacienteAsync(pacienteId);
        return agendamentos.Select(MapearParaDto);
    }

    public async Task<AgendamentoDto?> ObterPorIdAsync(Guid id)
    {
        var agendamento = await _agendamentoRepositorio.ObterComDetalheAsync(id);
        return agendamento is null ? null : MapearParaDto(agendamento);
    }

    public async Task<AgendamentoDto> CriarAsync(CriarAgendamentoDto dto)
    {
        var paciente = await _pacienteRepositorio.ObterPorIdAsync(dto.PacienteId)
            ?? throw new KeyNotFoundException("Paciente não encontrado.");

        if (paciente.Status == StatusGeral.Inativo)
            throw new InvalidOperationException("Paciente inativo não pode ser agendado.");

        var profissional = await _profissionalRepositorio.ObterComProcedimentosAsync(dto.ProfissionalId)
            ?? throw new KeyNotFoundException("Profissional não encontrado.");

        if (profissional.Status == StatusGeral.Inativo)
            throw new InvalidOperationException("Profissional inativo não pode receber agendamentos.");

        var procedimento = await _procedimentoRepositorio.ObterPorIdAsync(dto.ProcedimentoId)
            ?? throw new KeyNotFoundException("Procedimento não encontrado.");

        if (procedimento.Status == StatusGeral.Inativo)
            throw new InvalidOperationException("Procedimento inativo não pode ser agendado.");

        var habilitado = profissional.ProcedimentosHabilitados
            .Any(pp => pp.ProcedimentoId == dto.ProcedimentoId);

        if (!habilitado)
            throw new InvalidOperationException("Profissional não está habilitado para este procedimento.");

        var fim = dto.DataHoraInicio.AddMinutes(procedimento.DuracaoEstimadaMinutos);

        var conflito = await _agendamentoRepositorio.ExisteConflictoDeHorarioAsync(
            dto.ProfissionalId, dto.DataHoraInicio, fim);

        if (conflito)
            throw new InvalidOperationException("Profissional já possui agendamento neste horário.");

        var agendamento = new Agendamento
        {
            PacienteId = dto.PacienteId,
            ProfissionalId = dto.ProfissionalId,
            ProcedimentoId = dto.ProcedimentoId,
            DataHoraInicio = dto.DataHoraInicio,
            DataHoraFimPrevista = fim,
            Observacoes = dto.Observacoes,
            Status = StatusAgendamento.Agendado
        };

        await _agendamentoRepositorio.AdicionarAsync(agendamento);
        await _agendamentoRepositorio.SalvarAsync();

        return MapearParaDto(agendamento);
    }

    public async Task CancelarAsync(Guid id, CancelarAgendamentoDto dto)
    {
        var agendamento = await _agendamentoRepositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Agendamento não encontrado.");

        if (agendamento.Status == StatusAgendamento.Cancelado)
            throw new InvalidOperationException("Agendamento já está cancelado.");

        if (agendamento.Status == StatusAgendamento.Concluido)
            throw new InvalidOperationException("Agendamento já concluído não pode ser cancelado.");

        agendamento.Status = StatusAgendamento.Cancelado;
        agendamento.MotivoCancelamento = dto.MotivoCancelamento;
        agendamento.AtualizadoEm = DateTime.UtcNow;

        await _agendamentoRepositorio.AtualizarAsync(agendamento);
        await _agendamentoRepositorio.SalvarAsync();
    }

    public async Task AtualizarStatusAsync(Guid id, string novoStatus)
    {
        var agendamento = await _agendamentoRepositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Agendamento não encontrado.");

        if (!Enum.TryParse<StatusAgendamento>(novoStatus, out var status))
            throw new ArgumentException("Status inválido.");

        agendamento.Status = status;
        agendamento.AtualizadoEm = DateTime.UtcNow;

        await _agendamentoRepositorio.AtualizarAsync(agendamento);
        await _agendamentoRepositorio.SalvarAsync();
    }

    private static AgendamentoDto MapearParaDto(Agendamento a) => new()
    {
        Id = a.Id,
        PacienteId = a.PacienteId,
        NomePaciente = a.Paciente?.NomeCompleto ?? string.Empty,
        ProfissionalId = a.ProfissionalId,
        NomeProfissional = a.Profissional?.NomeCompleto ?? string.Empty,
        ProcedimentoId = a.ProcedimentoId,
        NomeProcedimento = a.Procedimento?.Nome ?? string.Empty,
        DataHoraInicio = a.DataHoraInicio,
        DataHoraFimPrevista = a.DataHoraFimPrevista,
        Status = a.Status.ToString(),
        Observacoes = a.Observacoes
    };
}