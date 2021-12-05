using Microsoft.EntityFrameworkCore.Migrations;

namespace ExercicioBiblioteca.Migrations
{
    public partial class InclusaoCampo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoEditora",
                table: "livros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoEditora",
                table: "livros");
        }
    }
}
