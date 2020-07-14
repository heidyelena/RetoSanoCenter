using Microsoft.EntityFrameworkCore.Migrations;

namespace ProyectoSanoCenter.Data.Migrations
{
    public partial class migracionEjercicios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetoId",
                table: "Ejercicio",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicio_RetoId",
                table: "Ejercicio",
                column: "RetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejercicio_Reto_RetoId",
                table: "Ejercicio",
                column: "RetoId",
                principalTable: "Reto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejercicio_Reto_RetoId",
                table: "Ejercicio");

            migrationBuilder.DropIndex(
                name: "IX_Ejercicio_RetoId",
                table: "Ejercicio");

            migrationBuilder.DropColumn(
                name: "RetoId",
                table: "Ejercicio");
        }
    }
}
