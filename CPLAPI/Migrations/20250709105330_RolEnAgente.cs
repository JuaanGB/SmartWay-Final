using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPLAPI.Migrations
{
    /// <inheritdoc />
    public partial class RolEnAgente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreUsuario",
                table: "Agentes",
                newName: "Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Agentes_Email",
                table: "Agentes",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agentes_Email",
                table: "Agentes");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Agentes",
                newName: "NombreUsuario");
        }
    }
}
