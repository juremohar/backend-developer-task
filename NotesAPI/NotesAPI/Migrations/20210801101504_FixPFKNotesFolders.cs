using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesAPI.Migrations
{
    public partial class FixPFKNotesFolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNoteFolder",
                table: "NotesFolders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdNoteFolder",
                table: "NotesFolders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
