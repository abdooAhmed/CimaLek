using Microsoft.EntityFrameworkCore.Migrations;

namespace CimaLek.Migrations
{
    public partial class filmserver5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_filmWatches_filmId",
                table: "filmWatches");

            migrationBuilder.AddColumn<int>(
                name: "LinkId",
                table: "films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_filmWatches_filmId",
                table: "filmWatches",
                column: "filmId",
                unique: true,
                filter: "[filmId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_filmWatches_filmId",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "LinkId",
                table: "films");

            migrationBuilder.CreateIndex(
                name: "IX_filmWatches_filmId",
                table: "filmWatches",
                column: "filmId");
        }
    }
}
