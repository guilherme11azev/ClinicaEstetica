using ClinicaEstetica.Application.DTOs.Paciente;
using ClinicaEstetica.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaEstetica.API.Controllers;

[Authorize]
public class PacientesController : BaseController
{
    private readonly IPacienteService _service;

    public PacientesController(IPacienteService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todos os pacientes ou filtra por termo de busca.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObterTodos([FromQuery] string? busca)
    {
        try
        {
            var resultado = string.IsNullOrWhiteSpace(busca)
                ? await _service.ObterTodosAsync()
                : await _service.BuscarAsync(busca);

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Retorna um paciente pelo ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var paciente = await _service.ObterPorIdAsync(id);
            return paciente is null ? NaoEncontrado("Paciente não encontrado.") : Ok(paciente);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Cria um novo paciente.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPacienteDto dto)
    {
        try
        {
            var paciente = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = paciente.Id }, paciente);
        }
        catch (InvalidOperationException ex)
        {
            return Conflito(ex.Message);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Atualiza os dados de um paciente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarPacienteDto dto)
    {
        try
        {
            await _service.AtualizarAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NaoEncontrado(ex.Message);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Inativa um paciente.
    /// </summary>
    [HttpPatch("{id:guid}/inativar")]
    public async Task<IActionResult> Inativar(Guid id)
    {
        try
        {
            await _service.InativarAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NaoEncontrado(ex.Message);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }
}