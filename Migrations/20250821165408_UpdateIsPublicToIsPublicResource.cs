using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentClassworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsPublicToIsPublicResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "VirtualFolders",
                newName: "IsPublicResource");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPublicResource",
                table: "VirtualFolders",
                newName: "IsPublic");
        }
    }
}
