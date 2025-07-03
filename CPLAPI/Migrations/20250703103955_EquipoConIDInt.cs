using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPLAPI.Migrations
{
    /// <inheritdoc />
    public partial class EquipoConIDInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agente_Equipos_EquipoId",
                table: "Agente");

            migrationBuilder.DropIndex(
                name: "IX_Agente_EquipoId",
                table: "Agente");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Equipos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "EquipoId1",
                table: "Agente",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agente_EquipoId1",
                table: "Agente",
                column: "EquipoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Agente_Equipos_EquipoId1",
                table: "Agente",
                column: "EquipoId1",
                principalTable: "Equipos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agente_Equipos_EquipoId1",
                table: "Agente");

            migrationBuilder.DropIndex(
                name: "IX_Agente_EquipoId1",
                table: "Agente");

            migrationBuilder.DropColumn(
                name: "EquipoId1",
                table: "Agente");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Equipos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Agente_EquipoId",
                table: "Agente",
                column: "EquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agente_Equipos_EquipoId",
                table: "Agente",
                column: "EquipoId",
                principalTable: "Equipos",
                principalColumn: "Id");
        }
    }
}
