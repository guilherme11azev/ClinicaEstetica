using ClinicaEstetica.Application.Interfaces;
using ClinicaEstetica.Application.Services;
using ClinicaEstetica.Domain.Interfaces;
using ClinicaEstetica.Infrastructure.Data;
using ClinicaEstetica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
builder.Services.AddScoped<IProfissionalRepositorio, ProfissionalRepositorio>();
builder.Services.AddScoped<IProcedimentoRepositorio, ProcedimentoRepositorio>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IAgendamentoRepositorio, AgendamentoRepositorio>();
builder.Services.AddScoped<IAtendimentoRepositorio, AtendimentoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IProfissionalService, ProfissionalService>();

// Services
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();