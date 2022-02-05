using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePageNet.App.Migrations
{
    public partial class Initialv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntities_AspNetUsers_ApplicationUserId",
                table: "PostEntities");

            migrationBuilder.DropIndex(
                name: "IX_PostEntities_ApplicationUserId",
                table: "PostEntities");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PostEntities");

            migrationBuilder.AlterColumn<string>(
                name: "PosterId",
                table: "PostEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntities_PosterId",
                table: "PostEntities",
                column: "PosterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntities_AspNetUsers_PosterId",
                table: "PostEntities",
                column: "PosterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostEntities_AspNetUsers_PosterId",
                table: "PostEntities");

            migrationBuilder.DropIndex(
                name: "IX_PostEntities_PosterId",
                table: "PostEntities");

            migrationBuilder.AlterColumn<string>(
                name: "PosterId",
                table: "PostEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PostEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostEntities_ApplicationUserId",
                table: "PostEntities",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostEntities_AspNetUsers_ApplicationUserId",
                table: "PostEntities",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
