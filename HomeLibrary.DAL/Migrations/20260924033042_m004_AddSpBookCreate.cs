using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m004_AddSpBookCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

    EXEC [dbo].Books_GetById @Id = @NewId;
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
