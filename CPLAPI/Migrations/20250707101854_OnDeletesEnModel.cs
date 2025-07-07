using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPLAPI.Migrations
{
    /// <inheritdoc />
    public partial class OnDeletesEnModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id");
        }
    }
}
