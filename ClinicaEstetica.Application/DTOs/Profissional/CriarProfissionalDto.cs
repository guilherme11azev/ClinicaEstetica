namespace ClinicaEstetica.Application.DTOs.Profissional;

public class CriarProfissionalDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Cargo { get; set; }
    public string? Especialidades { get; set; }
}