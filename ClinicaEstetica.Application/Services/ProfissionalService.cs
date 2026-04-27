using ClinicaEstetica.Application.DTOs.Profissional;
using ClinicaEstetica.Application.Interfaces;
using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Domain.Interfaces;

namespace ClinicaEstetica.Application.Services;

public class ProfissionalService : IProfissionalService
{
    private readonly IProfissionalRepositorio _repositorio;

    public ProfissionalService(IProfissionalRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<ProfissionalDto>> ObterTodosAsync()
    {
        var lista = await _repositorio.ObterTodosAsync();
        return lista.Select(MapearParaDto);
    }

    public async Task<IEnumerable<ProfissionalDto>> ObterAtivosAsync()
    {
        var lista = await _repositorio.ObterAtivosAsync();
        return lista.Select(MapearParaDto);
    }

    public async Task<ProfissionalDto?> ObterPorIdAsync(Guid id)
    {
        var profissional = await _repositorio.ObterPorIdAsync(id);
        return profissional is null ? null : MapearParaDto(profissional);
    }

    public async Task<ProfissionalDto> CriarAsync(CriarProfissionalDto dto)
    {
        var cpfExistente = await _repositorio.ObterPorCpfAsync(dto.Cpf);
        if (cpfExistente is not null)
            throw new InvalidOperationException("Já existe um profissional com esse CPF.");

        var profissional = new Profissional
        {
            NomeCompleto = dto.NomeCompleto,
            Cpf = dto.Cpf,
            Telefone = dto.Telefone,
            Email = dto.Email,
            Cargo = dto.Cargo,
            Especialidades = dto.Especialidades
        };

        await _repositorio.AdicionarAsync(profissional);
        await _repositorio.SalvarAsync();

        return MapearParaDto(profissional);
    }

    public async Task AtualizarAsync(Guid id, AtualizarProfissionalDto dto)
    {
        var profissional = await _repositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Profissional não encontrado.");

        profissional.NomeCompleto = dto.NomeCompleto;
        profissional.Telefone = dto.Telefone;
        profissional.Email = dto.Email;
        profissional.Cargo = dto.Cargo;
        profissional.Especialidades = dto.Especialidades;
        profissional.AtualizadoEm = DateTime.UtcNow;

        await _repositorio.AtualizarAsync(profissional);
        await _repositorio.SalvarAsync();
    }

    public async Task InativarAsync(Guid id)
    {
        var profissional = await _repositorio.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException("Profissional não encontrado.");

        profissional.Status = StatusGeral.Inativo;
        profissional.AtualizadoEm = DateTime.UtcNow;

        await _repositorio.AtualizarAsync(profissional);
        await _repositorio.SalvarAsync();
    }

    private static ProfissionalDto MapearParaDto(Profissional p) => new()
    {
        Id = p.Id,
        NomeCompleto = p.NomeCompleto,
        Cpf = p.Cpf,
        Telefone = p.Telefone,
        Email = p.Email,
        Cargo = p.Cargo,
        Especialidades = p.Especialidades,
        Status = p.Status.ToString()
    };
}