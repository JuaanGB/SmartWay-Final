using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPLAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedOperacionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Equipos_OperacionId",
                table: "Equipos",
                column: "OperacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Operaciones_OperacionId",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_OperacionId",
                table: "Equipos");
        }
    }
}
