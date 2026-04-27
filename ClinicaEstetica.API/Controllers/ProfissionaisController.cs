using ClinicaEstetica.Application.DTOs.Profissional;
using ClinicaEstetica.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaEstetica.API.Controllers;

public class ProfissionaisController : BaseController
{
    private readonly IProfissionalService _service;

    public ProfissionaisController(IProfissionalService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos([FromQuery] bool apenasAtivos = false)
    {
        try
        {
            var resultado = apenasAtivos
                ? await _service.ObterAtivosAsync()
                : await _service.ObterTodosAsync();

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var profissional = await _service.ObterPorIdAsync(id);
            return profissional is null ? NaoEncontrado("Profissional não encontrado.") : Ok(profissional);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProfissionalDto dto)
    {
        try
        {
            var profissional = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = profissional.Id }, profissional);
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarProfissionalDto dto)
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