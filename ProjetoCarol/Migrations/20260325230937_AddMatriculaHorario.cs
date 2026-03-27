using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCarol.Migrations
{
    /// <inheritdoc />
    public partial class AddMatriculaHorario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MatriculaHorarioId",
                table: "UsuarioAula",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "MatriculaHorario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioMatriculaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HorarioInicio = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    VigenteAPartirDe = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VigenteAte = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatriculaHorario_UsuarioMatricula_UsuarioMatriculaId",
                        column: x => x.UsuarioMatriculaId,
                        principalTable: "UsuarioMatricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAula_MatriculaHorarioId",
                table: "UsuarioAula",
                column: "MatriculaHorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaHorario_UsuarioMatriculaId",
                table: "MatriculaHorario",
                column: "UsuarioMatriculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioAula_MatriculaHorario_MatriculaHorarioId",
                table: "UsuarioAula",
                column: "MatriculaHorarioId",
                principalTable: "MatriculaHorario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioAula_MatriculaHorario_MatriculaHorarioId",
                table: "UsuarioAula");

            migrationBuilder.DropTable(
                name: "MatriculaHorario");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioAula_MatriculaHorarioId",
                table: "UsuarioAula");

            migrationBuilder.DropColumn(
                name: "MatriculaHorarioId",
                table: "UsuarioAula");
        }
    }
}
