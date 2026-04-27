using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaEstetica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pacientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCompleto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    WhatsApp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Endereco = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Genero = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "procedimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Categoria = table.Column<int>(type: "integer", nullable: false),
                    DuracaoEstimadaMinutos = table.Column<int>(type: "integer", nullable: false),
                    PrecoBase = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Requisitos = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Marca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UnidadeMedida = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    QuantidadeEstoque = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    CustoUnitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    DataValidade = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCompleto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Cargo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Especialidades = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movimentacoes_estoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentacoes_estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movimentacoes_estoque_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "procedimento_produtos",
                columns: table => new
                {
                    ProcedimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeEstimada = table.Column<decimal>(type: "numeric(10,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_procedimento_produtos", x => new { x.ProcedimentoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_procedimento_produtos_procedimentos_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "procedimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_procedimento_produtos_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "agendamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataHoraFimPrevista = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MotivoCancelamento = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_agendamentos_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_agendamentos_procedimentos_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "procedimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_agendamentos_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disponibilidades_profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disponibilidades_profissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_disponibilidades_profissionais_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profissional_procedimentos",
                columns: table => new
                {
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimentoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissional_procedimentos", x => new { x.ProfissionalId, x.ProcedimentoId });
                    table.ForeignKey(
                        name: "FK_profissional_procedimentos_procedimentos_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "procedimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profissional_procedimentos_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SenhaHash = table.Column<string>(type: "text", nullable: false),
                    Perfil = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuarios_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacoes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AtualizadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_atendimentos_agendamentos_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "agendamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_atendimentos_pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_atendimentos_profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "atendimento_produtos",
                columns: table => new
                {
                    AtendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantidadeUtilizada = table.Column<decimal>(type: "numeric(10,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atendimento_produtos", x => new { x.AtendimentoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_atendimento_produtos_atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_atendimento_produtos_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_PacienteId",
                table: "agendamentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_ProcedimentoId",
                table: "agendamentos",
                column: "ProcedimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_agendamentos_ProfissionalId",
                table: "agendamentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_atendimento_produtos_ProdutoId",
                table: "atendimento_produtos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_atendimentos_AgendamentoId",
                table: "atendimentos",
                column: "AgendamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_atendimentos_PacienteId",
                table: "atendimentos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_atendimentos_ProfissionalId",
                table: "atendimentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_disponibilidades_profissionais_ProfissionalId",
                table: "disponibilidades_profissionais",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentacoes_estoque_ProdutoId",
                table: "movimentacoes_estoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_pacientes_Cpf",
                table: "pacientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_procedimento_produtos_ProdutoId",
                table: "procedimento_produtos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_profissionais_Cpf",
                table: "profissionais",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_profissional_procedimentos_ProcedimentoId",
                table: "profissional_procedimentos",
                column: "ProcedimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_Email",
                table: "usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_ProfissionalId",
                table: "usuarios",
                column: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atendimento_produtos");

            migrationBuilder.DropTable(
                name: "disponibilidades_profissionais");

            migrationBuilder.DropTable(
                name: "movimentacoes_estoque");

            migrationBuilder.DropTable(
                name: "procedimento_produtos");

            migrationBuilder.DropTable(
                name: "profissional_procedimentos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "atendimentos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "agendamentos");

            migrationBuilder.DropTable(
                name: "pacientes");

            migrationBuilder.DropTable(
                name: "procedimentos");

            migrationBuilder.DropTable(
                name: "profissionais");
        }
    }
}
