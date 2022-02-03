using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePageNet.App.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaURI",
                table: "MessageEntities",
                newName: "MediaUri");

            migrationBuilder.RenameColumn(
                name: "MediaURI",
                table: "GroupEntities",
                newName: "MediaUri");

            migrationBuilder.RenameColumn(
                name: "MediaURI",
                table: "CommentEntities",
                newName: "MediaUri");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "UserRelationEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "UserReactionEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "UserGroupEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUri",
                table: "PostEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "PostEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "PostEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "MessageEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "GroupEntities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "CommentEntities",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "UserRelationEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "UserReactionEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "UserGroupEntities");

            migrationBuilder.DropColumn(
                name: "MediaUri",
                table: "PostEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "PostEntities");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "PostEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "MessageEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "GroupEntities");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "CommentEntities");

            migrationBuilder.RenameColumn(
                name: "MediaUri",
                table: "MessageEntities",
                newName: "MediaURI");

            migrationBuilder.RenameColumn(
                name: "MediaUri",
                table: "GroupEntities",
                newName: "MediaURI");

            migrationBuilder.RenameColumn(
                name: "MediaUri",
                table: "CommentEntities",
                newName: "MediaURI");
        }
    }
}
