using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FichaHemoparasitosis",
                schema: "DET",
                columns: table => new
                {
                    IdFichaHemo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoFichaHemo = table.Column<string>(type: "text", nullable: true),
                    IdHistoriaClinica = table.Column<long>(type: "bigint", nullable: false),
                    IdSintoma = table.Column<long>(type: "bigint", nullable: false),
                    IdEnfermedad = table.Column<long>(type: "bigint", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: true),
                    Tratamiento = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IpRegistro = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    IpModificacion = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    IpEliminacion = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    UsuarioRegistro = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    UsuarioModificacion = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichaHemoparasitosis", x => x.IdFichaHemo);
                    table.ForeignKey(
                        name: "FK_FichaHemoparasitosis_Enfermedad_IdEnfermedad",
                        column: x => x.IdEnfermedad,
                        principalSchema: "CAT",
                        principalTable: "Enfermedad",
                        principalColumn: "IdEnfermedad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaHemoparasitosis_HistoriaClinica_IdHistoriaClinica",
                        column: x => x.IdHistoriaClinica,
                        principalSchema: "DET",
                        principalTable: "HistoriaClinica",
                        principalColumn: "IdHistoriaClinica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichaHemoparasitosis_Sintomas_IdSintoma",
                        column: x => x.IdSintoma,
                        principalSchema: "CAT",
                        principalTable: "Sintomas",
                        principalColumn: "IdSintoma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FichaHemoparasitosis_IdEnfermedad",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "IdEnfermedad");

            migrationBuilder.CreateIndex(
                name: "IX_FichaHemoparasitosis_IdHistoriaClinica",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "IdHistoriaClinica");

            migrationBuilder.CreateIndex(
                name: "IX_FichaHemoparasitosis_IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "IdSintoma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FichaHemoparasitosis",
                schema: "DET");
        }
    }
}
