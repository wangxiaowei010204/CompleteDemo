using Microsoft.EntityFrameworkCore.Migrations;

namespace CompleteDemo.Migrations
{
    public partial class wwtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassExplain",
                table: "classinfo",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassExplain",
                table: "classinfo");
        }
    }
}
