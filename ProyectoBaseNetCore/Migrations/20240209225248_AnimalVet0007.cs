using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FichaHemoparasitosis_FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "FichaControlIdFichaControl");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaHemoparasitosis_FichaControl_FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "FichaControlIdFichaControl",
                principalSchema: "DET",
                principalTable: "FichaControl",
                principalColumn: "IdFichaControl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaHemoparasitosis_FichaControl_FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis");

            migrationBuilder.DropIndex(
                name: "IX_FichaHemoparasitosis_FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis");

            migrationBuilder.DropColumn(
                name: "FichaControlIdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis");

            migrationBuilder.DropColumn(
                name: "IdFichaControl",
                schema: "DET",
                table: "FichaHemoparasitosis");
        }
    }
}
