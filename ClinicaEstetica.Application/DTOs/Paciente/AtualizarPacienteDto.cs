namespace ClinicaEstetica.Application.DTOs.Paciente;

public class AtualizarPacienteDto
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? WhatsApp { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public string? Genero { get; set; }
    public string? Observacoes { get; set; }
}