using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesionales_Especialidades__especialidadId",
                table: "Profesionales");

            migrationBuilder.RenameColumn(
                name: "_especialidadId",
                table: "Profesionales",
                newName: "EspecialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_Profesionales__especialidadId",
                table: "Profesionales",
                newName: "IX_Profesionales_EspecialidadId");

            migrationBuilder.InsertData(
                table: "Especialidades",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Cardiología" },
                    { 2, "Dermatología" },
                    { 3, "Neurología" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Profesionales_Especialidades_EspecialidadId",
                table: "Profesionales",
                column: "EspecialidadId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profesionales_Especialidades_EspecialidadId",
                table: "Profesionales");

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Especialidades",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "EspecialidadId",
                table: "Profesionales",
                newName: "_especialidadId");

            migrationBuilder.RenameIndex(
                name: "IX_Profesionales_EspecialidadId",
                table: "Profesionales",
                newName: "IX_Profesionales__especialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profesionales_Especialidades__especialidadId",
                table: "Profesionales",
                column: "_especialidadId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
