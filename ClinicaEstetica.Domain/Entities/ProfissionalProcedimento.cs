namespace ClinicaEstetica.Domain.Entities;

public class ProfissionalProcedimento
{
    public Guid ProfissionalId { get; set; }
    public Profissional Profissional { get; set; } = null!;

    public Guid ProcedimentoId { get; set; }
    public Procedimento Procedimento { get; set; } = null!;
}