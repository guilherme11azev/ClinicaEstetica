using ClinicaEstetica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEstetica.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Procedimento> Procedimentos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }
    public DbSet<ProfissionalProcedimento> ProfissionalProcedimentos { get; set; }
    public DbSet<ProcedimentoProduto> ProcedimentoProdutos { get; set; }
    public DbSet<AtendimentoProduto> AtendimentoProdutos { get; set; }
    public DbSet<DisponibilidadeProfissional> DisponibilidadesProfissionais { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todos os mappings automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}