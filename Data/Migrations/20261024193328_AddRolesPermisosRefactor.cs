using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesPermisosRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrupoPermisoId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacturaId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EstadoFactura = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facturas_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GruposPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposPermisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetallesFactura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesFactura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetallesFactura_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoPermisoPermiso",
                columns: table => new
                {
                    GruposId = table.Column<int>(type: "int", nullable: false),
                    PermisosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPermisoPermiso", x => new { x.GruposId, x.PermisosId });
                    table.ForeignKey(
                        name: "FK_GrupoPermisoPermiso_GruposPermisos_GruposId",
                        column: x => x.GruposId,
                        principalTable: "GruposPermisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoPermisoPermiso_Permisos_PermisosId",
                        column: x => x.PermisosId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoPermisoId",
                table: "Usuarios",
                column: "GrupoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_FacturaId",
                table: "Turnos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesFactura_FacturaId",
                table: "DetallesFactura",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_TurnoId",
                table: "Facturas",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPermisoPermiso_PermisosId",
                table: "GrupoPermisoPermiso",
                column: "PermisosId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposPermisos_Nombre",
                table: "GruposPermisos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_Nombre_Categoria",
                table: "Permisos",
                columns: new[] { "Nombre", "Categoria" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Facturas_FacturaId",
                table: "Turnos",
                column: "FacturaId",
                principalTable: "Facturas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_GruposPermisos_GrupoPermisoId",
                table: "Usuarios",
                column: "GrupoPermisoId",
                principalTable: "GruposPermisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Facturas_FacturaId",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_GruposPermisos_GrupoPermisoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "DetallesFactura");

            migrationBuilder.DropTable(
                name: "GrupoPermisoPermiso");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "GruposPermisos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_GrupoPermisoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_FacturaId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "GrupoPermisoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FacturaId",
                table: "Turnos");
        }
    }
}
