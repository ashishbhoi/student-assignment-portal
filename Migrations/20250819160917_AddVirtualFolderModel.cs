using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentClassworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddVirtualFolderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VirtualFolderId",
                table: "UserFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VirtualFolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FolderName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualFolders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_VirtualFolderId",
                table: "UserFiles",
                column: "VirtualFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_VirtualFolders_VirtualFolderId",
                table: "UserFiles",
                column: "VirtualFolderId",
                principalTable: "VirtualFolders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_VirtualFolders_VirtualFolderId",
                table: "UserFiles");

            migrationBuilder.DropTable(
                name: "VirtualFolders");

            migrationBuilder.DropIndex(
                name: "IX_UserFiles_VirtualFolderId",
                table: "UserFiles");

            migrationBuilder.DropColumn(
                name: "VirtualFolderId",
                table: "UserFiles");
        }
    }
}
