using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m001_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    PublishYear = table.Column<int>(type: "int", nullable: false),
                    TableOfContents = table.Column<string>(type: "xml", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_Search];


GO

CREATE PROCEDURE [dbo].[Books_Search]
	@Query NVARCHAR(100) = NULL
AS
BEGIN
  select
    b.Id, b.Title, b.PublishYear, b.AuthorId, a.[Name] as AuthorName
  from Books b left join Authors a on a.Id = b.AuthorId
END
");


            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_GetById];


GO

CREATE PROCEDURE [dbo].[Books_GetById]
	@Id BIGINT
AS
BEGIN
  select
    b.Id, b.Title, b.PublishYear, b.AuthorId, a.[Name] as AuthorName, a.TableOfContent
  from Books b left join Authors a on a.Id = b.AuthorId
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
