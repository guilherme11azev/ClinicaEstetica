using ClinicaEstetica.Application.DTOs.Auth;
using ClinicaEstetica.Application.Interfaces;
using ClinicaEstetica.Domain.Entities;
using ClinicaEstetica.Domain.Enums;
using ClinicaEstetica.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClinicaEstetica.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _configuration = configuration;
    }

    public async Task<TokenDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepositorio.ObterPorEmailAsync(dto.Email)
            ?? throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

        if (usuario.Status == StatusGeral.Inativo)
            throw new UnauthorizedAccessException("Usuário inativo.");

        var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);
        if (!senhaValida)
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

        var token = GerarToken(usuario);
        var expiracao = DateTime.UtcNow.AddHours(
            int.Parse(_configuration["Jwt:ExpiracaoHoras"]!));

        return new TokenDto
        {
            Token = token,
            NomeUsuario = usuario.Nome,
            Perfil = usuario.Perfil.ToString(),
            Expiracao = expiracao
        };
    }

    public async Task CriarUsuarioAsync(CriarUsuarioDto dto)
    {
        var emailExistente = await _usuarioRepositorio.EmailJaCadastradoAsync(dto.Email);
        if (emailExistente)
            throw new InvalidOperationException("E-mail já cadastrado.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = senhaHash,
            Perfil = dto.Perfil,
            ProfissionalId = dto.ProfissionalId
        };

        await _usuarioRepositorio.AdicionarAsync(usuario);
        await _usuarioRepositorio.SalvarAsync();
    }

    private string GerarToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Perfil.ToString())
        };

        var expiracao = DateTime.UtcNow.AddHours(
            int.Parse(_configuration["Jwt:ExpiracaoHoras"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiracao,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}