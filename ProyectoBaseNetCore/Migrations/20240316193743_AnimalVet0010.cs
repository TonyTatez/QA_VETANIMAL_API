using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TablaContenido_IdFichaHemo",
                schema: "DET",
                table: "TablaContenido");

            migrationBuilder.CreateIndex(
                name: "IX_TablaContenido_IdFichaHemo",
                schema: "DET",
                table: "TablaContenido",
                column: "IdFichaHemo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TablaContenido_IdFichaHemo",
                schema: "DET",
                table: "TablaContenido");

            migrationBuilder.CreateIndex(
                name: "IX_TablaContenido_IdFichaHemo",
                schema: "DET",
                table: "TablaContenido",
                column: "IdFichaHemo");
        }
    }
}
