using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENOCA_CaseStudy.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
