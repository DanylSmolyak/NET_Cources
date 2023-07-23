using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7.Migrations.NewCatalogDb
{
    /// <inheritdoc />
    public partial class InitialNewCatalogMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemCatalog_SystemCatalog_SParentId",
                        column: x => x.SParentId,
                        principalTable: "SystemCatalog",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemCatalog_SParentId",
                table: "SystemCatalog",
                column: "SParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemCatalog");
        }
    }
}
