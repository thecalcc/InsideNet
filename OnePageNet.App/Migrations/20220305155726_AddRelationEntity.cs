using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnePageNet.App.Migrations
{
    public partial class AddRelationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRelationEntities_RelationEntity_UserRelationshipId",
                table: "UserRelationEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelationEntity",
                table: "RelationEntity");

            migrationBuilder.RenameTable(
                name: "RelationEntity",
                newName: "RelationEntities");

            migrationBuilder.AlterColumn<string>(
                name: "UserRelationshipId",
                table: "UserRelationEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelationEntities",
                table: "RelationEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelationEntities_RelationEntities_UserRelationshipId",
                table: "UserRelationEntities",
                column: "UserRelationshipId",
                principalTable: "RelationEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRelationEntities_RelationEntities_UserRelationshipId",
                table: "UserRelationEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelationEntities",
                table: "RelationEntities");

            migrationBuilder.RenameTable(
                name: "RelationEntities",
                newName: "RelationEntity");

            migrationBuilder.AlterColumn<string>(
                name: "UserRelationshipId",
                table: "UserRelationEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelationEntity",
                table: "RelationEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelationEntities_RelationEntity_UserRelationshipId",
                table: "UserRelationEntities",
                column: "UserRelationshipId",
                principalTable: "RelationEntity",
                principalColumn: "Id");
        }
    }
}
