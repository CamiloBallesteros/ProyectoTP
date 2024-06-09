using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTP.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    NumeroDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<int>(type: "int", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroCelular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime", nullable: false),
                    CiudadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.NumeroDocumento);
                    table.ForeignKey(
                        name: "FK_Clientes_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistroLlamadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razon = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FechaLlamada = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroLlamadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroLlamadas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "NumeroDocumento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CiudadId",
                table: "Clientes",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroLlamadas_ClienteId",
                table: "RegistroLlamadas",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroLlamadas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Ciudades");
        }
    }
}
