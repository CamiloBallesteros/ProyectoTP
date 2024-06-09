using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTP.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCityNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ciudades",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_Nombre",
                table: "Ciudades",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ciudades_Nombre",
                table: "Ciudades");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ciudades",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
