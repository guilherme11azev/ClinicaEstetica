using ClinicaEstetica.Domain.Enums;

namespace ClinicaEstetica.Domain.Entities;

public class Profissional : EntidadeBase
{
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Cargo { get; set; }
    public string? Especialidades { get; set; }
    public StatusGeral Status { get; set; } = StatusGeral.Ativo;

    // Navegação
    public ICollection<ProfissionalProcedimento> ProcedimentosHabilitados { get; set; } = [];
    public ICollection<Agendamento> Agendamentos { get; set; } = [];
    public ICollection<Atendimento> Atendimentos { get; set; } = [];
    public ICollection<DisponibilidadeProfissional> Disponibilidades { get; set; } = [];
}