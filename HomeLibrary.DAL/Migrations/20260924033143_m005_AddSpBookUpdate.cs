using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m005_AddSpBookUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_Update];

GO

CREATE PROCEDURE [dbo].[Books_Update]
    @Id               BIGINT,
    @Title            NVARCHAR(500),
    @PublishYear      INT,
    @TableOfContents  xml,
	@AuthorId		  BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [Authors] WHERE [Id] = @AuthorId)
        THROW 50001, N'Author not found.', 1;
    UPDATE [Books]
    SET [Title] = @Title,
	[PublishYear] = @PublishYear, 
	[TableOfContents] = @TableOfContents,
	[AuthorId] = @AuthorId
	WHERE [Id] = @Id
    SELECT @@ROWCOUNT
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
