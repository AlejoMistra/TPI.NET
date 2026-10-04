using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTurnoStateModelAndAddFechaHoraLlegada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaHoraLlegada",
                table: "Turnos",
                type: "datetime2",
                nullable: true);

            // Saneamiento de datos existentes: Mapear Cancelado a Libre y limpiar PacienteId
            migrationBuilder.Sql("UPDATE Turnos SET EstadoTurno = 'Libre', PacienteId = NULL WHERE EstadoTurno = 'Cancelado';");

            // Si las columnas temporales de cancelación existían en la BD, eliminarlas limpiamente
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Turnos') AND name = 'FechaCancelacion')
                BEGIN
                    ALTER TABLE Turnos DROP COLUMN FechaCancelacion;
                END
                IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Turnos') AND name = 'MotivoCancelacion')
                BEGIN
                    ALTER TABLE Turnos DROP COLUMN MotivoCancelacion;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaHoraLlegada",
                table: "Turnos");
        }
    }
}
