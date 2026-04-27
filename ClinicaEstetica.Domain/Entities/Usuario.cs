using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Usuario : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; }
    public StatusGeral Status { get; set; } = StatusGeral.Ativo;

    // Relacionamento com Profissional (opcional)
    public Guid? ProfissionalId { get; set; }
    public Profissional? Profissional { get; set; }
}