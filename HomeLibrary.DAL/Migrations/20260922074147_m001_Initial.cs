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
    b.[Id], b.[Title], b.[PublishYear], b.[AuthorId], a.[Id], a.[Name]
  from Books b left join Authors a on a.[Id] = b.AuthorId
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
  select top(1)
    b.[Id], b.[Title], b.[PublishYear], b.[AuthorId], a.[Id], a.[Name], b.[TableOfContents]
  from Books b left join Authors a on a.[Id] = b.[AuthorId]
  where b.[Id] = @Id
END
");

            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_Create];

GO

CREATE PROCEDURE [dbo].[Books_Create]
    @Title            NVARCHAR(500),
    @PublishYear      INT,
    @TableOfContents  xml,
	@AuthorId		  BIGINT
AS

BEGIN
	DECLARE @NewId BIGINT;
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [Authors] WHERE [Id] = @AuthorId)
        THROW 50001, N'Author not found.', 1;
    INSERT INTO [Books] ([Title], [AuthorId], [PublishYear], [TableOfContents])
    VALUES (@Title, @AuthorId, @PublishYear, @TableOfContents);
    SET @NewId = SCOPE_IDENTITY();

  select top(1)
    b.[Id], b.[Title], b.[PublishYear], b.[AuthorId], a.[Id], a.[Name], b.[TableOfContents]
  from Books b left join Authors a on a.[Id] = b.[AuthorId]
  where b.[Id] = @NewId
END;

");

            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Authors_FindByName];

GO

CREATE PROCEDURE [dbo].[Authors_FindByName]
	@Name VARCHAR(200)
AS
BEGIN
  select top(1)
    a.[Id], a.[Name]
  from Authors a 
  where a.[Name] = @Name
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
