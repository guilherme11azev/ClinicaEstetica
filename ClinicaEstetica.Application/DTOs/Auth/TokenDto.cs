namespace ClinicaEstetica.Application.DTOs.Auth;

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
    public string NomeUsuario { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public DateTime Expiracao { get; set; }
}