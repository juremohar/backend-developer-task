using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesAPI.Migrations
{
    public partial class AddFolderToNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFolder",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdFolder",
                table: "Notes",
                column: "IdFolder");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Folders_IdFolder",
                table: "Notes",
                column: "IdFolder",
                principalTable: "Folders",
                principalColumn: "IdFolder",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Folders_IdFolder",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdFolder",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IdFolder",
                table: "Notes");
        }
    }
}
