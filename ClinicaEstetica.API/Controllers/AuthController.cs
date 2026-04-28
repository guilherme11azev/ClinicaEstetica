using ClinicaEstetica.Application.DTOs.Auth;
using ClinicaEstetica.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaEstetica.API.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    /// <summary>
    /// Realiza o login e retorna o token JWT.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _service.LoginAsync(dto);
            return Ok(token);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            return ErroInterno(ex);
        }
    }

    /// <summary>
    /// Cria um novo usuário. Apenas Administrador pode executar.
    /// </summary>
    [HttpPost("usuarios")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioDto dto)
    {
        try
        {
            await _service.CriarUsuarioAsync(dto);
            return Created(string.Empty, new { mensagem = "Usuário criado com sucesso." });
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
}