using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCG.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCategoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    TipoCategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_TipoCategoria_TipoCategoriaId",
                        column: x => x.TipoCategoriaId,
                        principalTable: "TipoCategoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Costo = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Balance_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoCategoria",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 1, "Ingreso" });

            migrationBuilder.InsertData(
                table: "TipoCategoria",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 2, "Gasto" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nombre", "TipoCategoriaId" },
                values: new object[] { 1, "Sueldo", 1 });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nombre", "TipoCategoriaId" },
                values: new object[] { 2, "Gasto Mensuales", 2 });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nombre", "TipoCategoriaId" },
                values: new object[] { 3, "Gasto Diarios", 2 });

            migrationBuilder.InsertData(
                table: "Balance",
                columns: new[] { "Id", "CategoriaId", "Costo", "Descripcion", "Fecha" },
                values: new object[] { 1, 1, 85000, "Primera Quincena", new DateTime(2021, 2, 20, 16, 35, 14, 556, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Balance",
                columns: new[] { "Id", "CategoriaId", "Costo", "Descripcion", "Fecha" },
                values: new object[] { 2, 2, 9000, "Pago de Reparaciones", new DateTime(2021, 2, 25, 16, 35, 14, 557, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Balance_CategoriaId",
                table: "Balance",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_TipoCategoriaId",
                table: "Categoria",
                column: "TipoCategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "TipoCategoria");
        }
    }
}
