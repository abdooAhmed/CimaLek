using Microsoft.EntityFrameworkCore.Migrations;

namespace CimaLek.Migrations
{
    public partial class Author1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorToFilms",
                columns: table => new
                {
                    filmId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    authorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorToFilms", x => new { x.filmId, x.authorId });
                    table.ForeignKey(
                        name: "FK_AuthorToFilms_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorToFilms_films_filmId",
                        column: x => x.filmId,
                        principalTable: "films",
                        principalColumn: "filmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorToSeries",
                columns: table => new
                {
                    serieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    authorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorToSeries", x => new { x.serieId, x.authorId });
                    table.ForeignKey(
                        name: "FK_AuthorToSeries_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorToSeries_serie_serieId",
                        column: x => x.serieId,
                        principalTable: "serie",
                        principalColumn: "seriesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorToFilms_authorId",
                table: "AuthorToFilms",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorToSeries_authorId",
                table: "AuthorToSeries",
                column: "authorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorToFilms");

            migrationBuilder.DropTable(
                name: "AuthorToSeries");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
