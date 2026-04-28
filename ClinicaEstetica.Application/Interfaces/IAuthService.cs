using ClinicaEstetica.Application.DTOs.Auth;

namespace ClinicaEstetica.Application.Interfaces;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(LoginDto dto);
    Task CriarUsuarioAsync(CriarUsuarioDto dto);
}