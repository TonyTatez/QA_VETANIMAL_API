using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VET_ANIMAL_API.Migrations
{
    /// <inheritdoc />
    public partial class AnimalVet0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaHemoparasitosis_Sintomas_IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis");

            migrationBuilder.AlterColumn<long>(
                name: "IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_FichaHemoparasitosis_Sintomas_IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "IdSintoma",
                principalSchema: "CAT",
                principalTable: "Sintomas",
                principalColumn: "IdSintoma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FichaHemoparasitosis_Sintomas_IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis");

            migrationBuilder.AlterColumn<long>(
                name: "IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FichaHemoparasitosis_Sintomas_IdSintoma",
                schema: "DET",
                table: "FichaHemoparasitosis",
                column: "IdSintoma",
                principalSchema: "CAT",
                principalTable: "Sintomas",
                principalColumn: "IdSintoma",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
