namespace ClinicaEstetica.Domain.Entities;

public abstract class EntidadeBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime? AtualizadoEm { get; set; }
    public Guid? CriadoPorId { get; set; }
    public Guid? AtualizadoPorId { get; set; }
}