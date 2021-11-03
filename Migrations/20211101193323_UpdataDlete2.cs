using Microsoft.EntityFrameworkCore.Migrations;

namespace CimaLek.Migrations
{
    public partial class UpdataDlete2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "Link1",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "Link3",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "Link4",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "ServerName1",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "ServerName2",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "ServerName3",
                table: "filmWatches");

            migrationBuilder.DropColumn(
                name: "ServerName4",
                table: "filmWatches");

            migrationBuilder.AlterColumn<string>(
                name: "filmId",
                table: "filmWatches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches",
                column: "filmId",
                principalTable: "films",
                principalColumn: "filmId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches");

            migrationBuilder.AlterColumn<string>(
                name: "filmId",
                table: "filmWatches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link3",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link4",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerName1",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerName2",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerName3",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerName4",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches",
                column: "filmId",
                principalTable: "films",
                principalColumn: "filmId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
