using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veterinario",
                schema: "CAT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Identificacion = table.Column<string>(type: "text", nullable: true),
                    Sede = table.Column<string>(type: "text", nullable: true),
                    Rol = table.Column<string>(type: "text", nullable: true),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    Correo = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Veterinario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veterinario",
                schema: "CAT");
        }
    }
}
