using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentClassworkPortal.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserIdNullableInUserFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFiles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
