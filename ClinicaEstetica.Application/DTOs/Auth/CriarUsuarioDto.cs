using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Application.DTOs.Auth;

public class CriarUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; }
    public Guid? ProfissionalId { get; set; }
}