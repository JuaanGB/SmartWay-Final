using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPLAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgentesAnadidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agente_Equipos_EquipoId",
                table: "Agente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agente",
                table: "Agente");

            migrationBuilder.RenameTable(
                name: "Agente",
                newName: "Agentes");

            migrationBuilder.RenameIndex(
                name: "IX_Agente_EquipoId",
                table: "Agentes",
                newName: "IX_Agentes_EquipoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agentes",
                table: "Agentes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agentes_Equipos_EquipoId",
                table: "Agentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agentes",
                table: "Agentes");

            migrationBuilder.RenameTable(
                name: "Agentes",
                newName: "Agente");

            migrationBuilder.RenameIndex(
                name: "IX_Agentes_EquipoId",
                table: "Agente",
                newName: "IX_Agente_EquipoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agente",
                table: "Agente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agente_Equipos_EquipoId",
                table: "Agente",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");
        }
    }
}
