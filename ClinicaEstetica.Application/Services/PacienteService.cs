using ClinicaEstetica.Application.DTOs.Paciente;
using ClinicaEstetica.Application.Interfaces;
using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Domain.Interfaces;

namespace ClinicaEstetica.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly IPacienteRepositorio _repositorio;

    public PacienteService(IPacienteRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<PacienteDto>> ObterTodosAsync()
    {
        var pacientes = await _repositorio.ObterTodosAsync();
        return pacientes.Select(MapearParaDto);
    }

    public async Task<PacienteDto?> ObterPorIdAsync(Guid id)
    {
        var paciente = await _repositorio.ObterPorIdAsync(id);
        return paciente is null ? null : MapearParaDto(paciente);
    }

    public async Task<IEnumerable<PacienteDto>> BuscarAsync(string termo)
    {
        var pacientes = await _repositorio.BuscarAsync(termo);
        return pacientes.Select(MapearParaDto);
    }

    public async Task<PacienteDto> CriarAsync(CriarPacienteDto dto)
    {
        var cpfExistente = await _repositorio.ObterPorCpfAsync(dto.Cpf);
        if (cpfExistente is not null)
            throw new InvalidOperationException("Já existe um paciente com esse CPF.");

        var paciente = new Paciente
        {
            NomeCompleto = dto.NomeCompleto,
            Cpf = dto.Cpf,
            DataNascimento = dto.DataNascimento,
            Telefone = dto.Telefone,
            WhatsApp = dto.WhatsApp,
            Email = dto.Email,
            Endereco = dto.Endereco,
            Genero = dto.Genero,
            Observacoes = dto.Observacoes
        };

        await _repositorio.AdicionarAsync(paciente);
        await _repositorio.SalvarAsync();

        return MapearParaDto(paciente);
    }

    public async Task AtualizarAsync(Guid id, AtualizarPacienteDto dto)
    {
        var paciente = await _repositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Paciente não encontrado.");

        paciente.NomeCompleto = dto.NomeCompleto;
        paciente.Telefone = dto.Telefone;
        paciente.WhatsApp = dto.WhatsApp;
        paciente.Email = dto.Email;
        paciente.Endereco = dto.Endereco;
        paciente.Genero = dto.Genero;
        paciente.Observacoes = dto.Observacoes;
        paciente.AtualizadoEm = DateTime.UtcNow;

        await _repositorio.AtualizarAsync(paciente);
        await _repositorio.SalvarAsync();
    }

    public async Task InativarAsync(Guid id)
    {
        var paciente = await _repositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Paciente não encontrado.");

        paciente.Status = StatusGeral.Inativo;
        paciente.AtualizadoEm = DateTime.UtcNow;

        await _repositorio.AtualizarAsync(paciente);
        await _repositorio.SalvarAsync();
    }

    private static PacienteDto MapearParaDto(Paciente p) => new()
    {
        Id = p.Id,
        NomeCompleto = p.NomeCompleto,
        Cpf = p.Cpf,
        DataNascimento = p.DataNascimento,
        Telefone = p.Telefone,
        WhatsApp = p.WhatsApp,
        Email = p.Email,
        Endereco = p.Endereco,
        Genero = p.Genero,
        Observacoes = p.Observacoes,
        Status = p.Status.ToString(),
        CriadoEm = p.CriadoEm
    };
}