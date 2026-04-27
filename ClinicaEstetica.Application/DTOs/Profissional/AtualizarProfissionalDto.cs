namespace ClinicaEstetica.Application.DTOs.Profissional;

public class AtualizarProfissionalDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Cargo { get; set; }
    public string? Especialidades { get; set; }
}