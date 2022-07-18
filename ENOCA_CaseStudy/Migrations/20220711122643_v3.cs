using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENOCA_CaseStudy.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "MovieSaloon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "MovieSaloon");
        }
    }
}
