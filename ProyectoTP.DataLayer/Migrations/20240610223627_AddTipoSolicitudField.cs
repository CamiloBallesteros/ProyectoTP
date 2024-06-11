using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTP.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoSolicitudField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoSolicitud",
                table: "RegistroLlamadas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoSolicitud",
                table: "RegistroLlamadas");
        }
    }
}
