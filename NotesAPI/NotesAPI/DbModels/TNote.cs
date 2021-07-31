using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TNote
    {
        [Key]
        public int IdNote { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [ForeignKey("NoteVisibility")]
        public int IdNoteVisibility { get; set; }

        [ForeignKey("NoteBodyType")]
        public int IdNoteBodyType { get; set; }

        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertedAt { get; set; }

        public TUser User { get; set; }

        public TNoteVisibility NoteVisibility { get; set; }

        public List<TNoteBody> NoteBodies { get; set; }

        public TNoteBodyType NoteBodyType { get; set; }
    }
}
