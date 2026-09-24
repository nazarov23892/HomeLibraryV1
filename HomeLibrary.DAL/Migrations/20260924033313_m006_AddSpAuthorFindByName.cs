using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m006_AddSpAuthorFindByName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
