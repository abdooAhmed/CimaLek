using Microsoft.EntityFrameworkCore.Migrations;

namespace CimaLek.Migrations
{
    public partial class film : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anime",
                table: "films");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "anime",
                table: "films",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
