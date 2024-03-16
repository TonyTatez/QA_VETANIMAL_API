using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TablaContenido",
                schema: "DET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true),
                    IdFichaHemo = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TablaContenido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaContenido_FichaHemoparasitosis_IdFichaHemo",
                        column: x => x.IdFichaHemo,
                        principalSchema: "DET",
                        principalTable: "FichaHemoparasitosis",
                        principalColumn: "IdFichaHemo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TablaContenido_IdFichaHemo",
                schema: "DET",
                table: "TablaContenido",
                column: "IdFichaHemo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaContenido",
                schema: "DET");
        }
    }
}
