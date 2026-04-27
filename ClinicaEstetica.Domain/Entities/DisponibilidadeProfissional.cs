namespace ClinicaEstetica.Domain.Entities;

public class DisponibilidadeProfissional
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; } = null!;

    public DayOfWeek DiaSemana { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFim { get; set; }
}