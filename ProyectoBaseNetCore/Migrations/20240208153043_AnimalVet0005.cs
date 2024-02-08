using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdRaza",
                schema: "CAT",
                table: "Mascotas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Razas",
                columns: table => new
                {
                    IdRaza = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Razas", x => x.IdRaza);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas",
                column: "RazaNuevaIdRaza");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Razas_RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas",
                column: "RazaNuevaIdRaza",
                principalTable: "Razas",
                principalColumn: "IdRaza");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Razas_RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "Razas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "IdRaza",
                schema: "CAT",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "RazaNuevaIdRaza",
                schema: "CAT",
                table: "Mascotas");
        }
    }
}
