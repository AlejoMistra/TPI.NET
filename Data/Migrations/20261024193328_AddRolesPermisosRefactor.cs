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
                name: "FK_Usuarios_GruposPermisos_GrupoPermisoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "GrupoPermisoPermiso");

            migrationBuilder.DropTable(
                name: "GruposPermisos");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_GrupoPermisoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "GrupoPermisoId",
                table: "Usuarios");
        }
    }
}
