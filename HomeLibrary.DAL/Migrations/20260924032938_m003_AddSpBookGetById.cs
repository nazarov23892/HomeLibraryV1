using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m003_AddSpBookGetById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_GetById];


GO

CREATE PROCEDURE [dbo].[Books_GetById]
	@Id BIGINT
AS
BEGIN
  select top(1)
    b.[Id], b.[Title], b.[PublishYear],b.[TableOfContents], b.[AuthorId]
    , a.[Id], a.[Name]
  from Books b left join Authors a on a.[Id] = b.[AuthorId]
  where b.[Id] = @Id
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
