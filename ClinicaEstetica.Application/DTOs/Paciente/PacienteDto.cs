namespace ClinicaEstetica.Application.DTOs.Paciente;

public class PacienteDto
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string? WhatsApp { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public string? Genero { get; set; }
    public string? Observacoes { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
}