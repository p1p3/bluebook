using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Infrastructure.Database.Migrations
{
    public partial class InitialApplicationDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "books_managment");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "books_managment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fieldtypes",
                schema: "books_managment",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fieldtypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                schema: "books_managment",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    ChapterNumber = table.Column<int>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chapters_books_BookId",
                        column: x => x.BookId,
                        principalSchema: "books_managment",
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                schema: "books_managment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChapterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pages_chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalSchema: "books_managment",
                        principalTable: "chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "page_fields",
                schema: "books_managment",
                columns: table => new
                {
                    Identifier = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    PageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_fields", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_page_fields_pages_PageId",
                        column: x => x.PageId,
                        principalSchema: "books_managment",
                        principalTable: "pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_page_fields_fieldtypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "books_managment",
                        principalTable: "fieldtypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chapters_BookId",
                schema: "books_managment",
                table: "chapters",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_page_fields_PageId",
                schema: "books_managment",
                table: "page_fields",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_page_fields_TypeId",
                schema: "books_managment",
                table: "page_fields",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_ChapterId",
                schema: "books_managment",
                table: "pages",
                column: "ChapterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "page_fields",
                schema: "books_managment");

            migrationBuilder.DropTable(
                name: "pages",
                schema: "books_managment");

            migrationBuilder.DropTable(
                name: "fieldtypes",
                schema: "books_managment");

            migrationBuilder.DropTable(
                name: "chapters",
                schema: "books_managment");

            migrationBuilder.DropTable(
                name: "books",
                schema: "books_managment");
        }
    }
}
