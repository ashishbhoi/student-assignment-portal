using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentClassworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentPropertiesToVirtualFolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FolderName",
                table: "VirtualFolders",
                newName: "Chapter");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentName",
                table: "VirtualFolders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Book",
                table: "VirtualFolders",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "VirtualFolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "VirtualFolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "VirtualFolders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentName",
                table: "VirtualFolders");

            migrationBuilder.DropColumn(
                name: "Book",
                table: "VirtualFolders");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "VirtualFolders");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "VirtualFolders");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "VirtualFolders");

            migrationBuilder.RenameColumn(
                name: "Chapter",
                table: "VirtualFolders",
                newName: "FolderName");
        }
    }
}
