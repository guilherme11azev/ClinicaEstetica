using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Paciente : EntidadeBase
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string? WhatsApp { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public string? Genero { get; set; }
    public string? Observacoes { get; set; }
    public StatusGeral Status { get; set; } = StatusGeral.Ativo;

    // Navegação
    public ICollection<Agendamento> Agendamentos { get; set; } = [];
    public ICollection<Atendimento> Atendimentos { get; set; } = [];
}