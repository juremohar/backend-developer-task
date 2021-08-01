using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesAPI.Migrations
{
    public partial class CreateNotesFoldersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "NotesFolders",
                columns: table => new
                {
                    IdNote = table.Column<int>(type: "int", nullable: false),
                    IdFolder = table.Column<int>(type: "int", nullable: false),
                    IdNoteFolder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesFolders", x => new { x.IdNote, x.IdFolder });
                    table.ForeignKey(
                        name: "FK_NotesFolders_Folders_IdFolder",
                        column: x => x.IdFolder,
                        principalTable: "Folders",
                        principalColumn: "IdFolder",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesFolders_Notes_IdNote",
                        column: x => x.IdNote,
                        principalTable: "Notes",
                        principalColumn: "IdNote",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NotesFolders_IdFolder",
                table: "NotesFolders",
                column: "IdFolder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesFolders");

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
    }
}
