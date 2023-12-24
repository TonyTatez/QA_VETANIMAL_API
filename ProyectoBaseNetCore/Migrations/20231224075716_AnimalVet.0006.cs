using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoBaseNetCore.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                schema: "CAT",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                schema: "CAT",
                table: "Clientes");
        }
    }
}
