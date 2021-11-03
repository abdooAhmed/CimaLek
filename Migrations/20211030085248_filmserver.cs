using Microsoft.EntityFrameworkCore.Migrations;

namespace CimaLek.Migrations
{
    public partial class filmserver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmWatch_films_filmId",
                table: "filmWatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_filmWatch",
                table: "filmWatch");

            migrationBuilder.RenameTable(
                name: "filmWatch",
                newName: "filmWatches");

            migrationBuilder.RenameIndex(
                name: "IX_filmWatch_filmId",
                table: "filmWatches",
                newName: "IX_filmWatches_filmId");

            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: true);

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
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerName4",
                table: "filmWatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_filmWatches",
                table: "filmWatches",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches",
                column: "filmId",
                principalTable: "films",
                principalColumn: "filmId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filmWatches_films_filmId",
                table: "filmWatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_filmWatches",
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

            migrationBuilder.RenameTable(
                name: "filmWatches",
                newName: "filmWatch");

            migrationBuilder.RenameIndex(
                name: "IX_filmWatches_filmId",
                table: "filmWatch",
                newName: "IX_filmWatch_filmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_filmWatch",
                table: "filmWatch",
                column: "LinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_filmWatch_films_filmId",
                table: "filmWatch",
                column: "filmId",
                principalTable: "films",
                principalColumn: "filmId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
