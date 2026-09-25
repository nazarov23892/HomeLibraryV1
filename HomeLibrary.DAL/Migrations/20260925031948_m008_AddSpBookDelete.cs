using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m008_AddSpBookDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Books_Delete];

GO

CREATE PROCEDURE [dbo].[Books_Delete]
    @Id               BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [Books]
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
