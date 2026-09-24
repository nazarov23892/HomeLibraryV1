using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeLibrary.DAL.Migrations
{
    /// <inheritdoc />
    public partial class m007_AddSpAuthorCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"
DROP PROCEDURE IF EXISTS [dbo].[Authors_Create];

GO

CREATE PROCEDURE [dbo].[Authors_Create]
    @Name NVARCHAR(200)
AS

BEGIN
	DECLARE @NewId BIGINT;
    SET NOCOUNT ON;

    INSERT INTO [Authors] ([Name])
    VALUES (@Name);
    SET @NewId = SCOPE_IDENTITY();

    select top(1) 
	a.[Id], a.[Name]
	from [Authors] a 
	where a.[Id] = @NewId
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
