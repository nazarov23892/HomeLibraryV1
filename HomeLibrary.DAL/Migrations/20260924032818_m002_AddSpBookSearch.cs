using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m002_AddSpBookSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_Search];


GO

CREATE PROCEDURE [dbo].[Books_Search]
    @PageStartsZero int = 0,
	@PerPage int = 10,
	@SearchString NVARCHAR(100) = NULL,
	@TotalCount INT OUTPUT
AS
BEGIN
  SELECT @TotalCount = COUNT(*) 
  FROM Books b inner join Authors a on a.[Id] = b.[AuthorId]
  WHERE @SearchString IS NULL 
  OR LOWER(b.[Title]) LIKE CONCAT('%', LOWER(@SearchString), '%')
  OR LOWER(a.[Name]) LIKE CONCAT('%', LOWER(@SearchString), '%')

  SELECT
    b.[Id], b.[Title], b.[PublishYear], b.[AuthorId], a.[Id], a.[Name], COUNT(*) OVER()
  FROM Books b inner join Authors a on a.[Id] = b.[AuthorId]
  WHERE @SearchString IS NULL 
  OR LOWER(b.[Title]) LIKE CONCAT('%', LOWER(@SearchString), '%')
  OR LOWER(a.[Name]) LIKE CONCAT('%', LOWER(@SearchString), '%')
  ORDER by  b.[Id]
  OFFSET @PageStartsZero * @PerPage  ROWS FETCH NEXT @PerPage ROWS ONLY
END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
