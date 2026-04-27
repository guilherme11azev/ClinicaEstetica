using Microsoft.AspNetCore.Mvc;

namespace ClinicaEstetica.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ErroInterno(Exception ex)
        => StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });

    protected IActionResult NaoEncontrado(string mensagem)
        => NotFound(new { mensagem });

    protected IActionResult Conflito(string mensagem)
        => Conflict(new { mensagem });

    protected IActionResult Invalido(string mensagem)
        => BadRequest(new { mensagem });
}