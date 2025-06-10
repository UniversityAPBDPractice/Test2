using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    IdAuthor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.IdAuthor);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    IdGenre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.IdGenre);
                });

            migrationBuilder.CreateTable(
                name: "PublishingHouse",
                columns: table => new
                {
                    IdPublishingHouse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    City = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishingHouse", x => x.IdPublishingHouse);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    IdBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPublishingHouse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.IdBook);
                    table.ForeignKey(
                        name: "FK_Book_PublishingHouse_IdPublishingHouse",
                        column: x => x.IdPublishingHouse,
                        principalTable: "PublishingHouse",
                        principalColumn: "IdPublishingHouse",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    IdAuthor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => new { x.IdBook, x.IdAuthor });
                    table.ForeignKey(
                        name: "FK_BookAuthor_Author_IdAuthor",
                        column: x => x.IdAuthor,
                        principalTable: "Author",
                        principalColumn: "IdAuthor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthor_Book_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Book",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    IdBook = table.Column<int>(type: "int", nullable: false),
                    IdGenre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.IdBook, x.IdGenre });
                    table.ForeignKey(
                        name: "FK_BookGenre_Book_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Book",
                        principalColumn: "IdBook",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genre_IdGenre",
                        column: x => x.IdGenre,
                        principalTable: "Genre",
                        principalColumn: "IdGenre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "IdAuthor", "FirstName", "LastName" },
                values: new object[] { 1, "Sanya", "Milko" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "IdGenre", "Name" },
                values: new object[] { 1, "My fav genre" });

            migrationBuilder.InsertData(
                table: "PublishingHouse",
                columns: new[] { "IdPublishingHouse", "City", "Country", "Name" },
                values: new object[] { 1, "London", "UK", "Sanya Press" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "IdBook", "IdPublishingHouse", "Name", "ReleaseDate" },
                values: new object[] { 1, 1, "My book", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "IdAuthor", "IdBook" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "IdBook", "IdGenre" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Book_IdPublishingHouse",
                table: "Book",
                column: "IdPublishingHouse");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_IdAuthor",
                table: "BookAuthor",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_IdGenre",
                table: "BookGenre",
                column: "IdGenre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "PublishingHouse");
        }
    }
}
