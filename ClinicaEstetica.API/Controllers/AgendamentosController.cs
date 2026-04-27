using ClinicaEstetica.Application.DTOs.Agendamento;
using ClinicaEstetica.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaEstetica.API.Controllers;

public class AgendamentosController : BaseController
{
    private readonly IAgendamentoService _service;

    public AgendamentosController(IAgendamentoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna agendamentos por período.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> ObterPorPeriodo(
        [FromQuery] DateTime inicio,
        [FromQuery] DateTime fim)
    {
        try
        {
            var resultado = await _service.ObterPorPeriodoAsync(inicio, fim);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Retorna agendamentos de um profissional em uma data.
    /// </summary>
    [HttpGet("profissional/{profissionalId:guid}")]
    public async Task<IActionResult> ObterPorProfissional(
        Guid profissionalId,
        [FromQuery] DateOnly data)
    {
        try
        {
            var resultado = await _service.ObterPorProfissionalAsync(profissionalId, data);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Retorna agendamentos de um paciente.
    /// </summary>
    [HttpGet("paciente/{pacienteId:guid}")]
    public async Task<IActionResult> ObterPorPaciente(Guid pacienteId)
    {
        try
        {
            var resultado = await _service.ObterPorPacienteAsync(pacienteId);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Retorna um agendamento pelo ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        try
        {
            var agendamento = await _service.ObterPorIdAsync(id);
            return agendamento is null ? NaoEncontrado("Agendamento não encontrado.") : Ok(agendamento);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Cria um novo agendamento com validação de conflito de horário.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAgendamentoDto dto)
    {
        try
        {
            var agendamento = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = agendamento.Id }, agendamento);
        }
        catch (KeyNotFoundException ex)
        {
            return NaoEncontrado(ex.Message);
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
    /// Cancela um agendamento registrando o motivo.
    /// </summary>
    [HttpPatch("{id:guid}/cancelar")]
    public async Task<IActionResult> Cancelar(Guid id, [FromBody] CancelarAgendamentoDto dto)
    {
        try
        {
            await _service.CancelarAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NaoEncontrado(ex.Message);
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
    /// Atualiza o status de um agendamento.
    /// </summary>
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> AtualizarStatus(Guid id, [FromBody] string novoStatus)
    {
        try
        {
            await _service.AtualizarStatusAsync(id, novoStatus);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NaoEncontrado(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return Invalido(ex.Message);
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }
}