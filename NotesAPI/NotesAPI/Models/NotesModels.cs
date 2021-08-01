using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Models
{
    public class InsertNoteModel 
    {
        public int? IdFolder { get; set; }
        public string VisibilityCode { get; set; }
        public string Title { get; set; }
        public string NoteBodyTypeCode { get; set; }
        public List<string> Body { get; set; }
    }

    public class UpdateNoteModel 
    {
        public int? IdFolder { get; set; }
        public string VisibilityCode { get; set; }
        public string Title { get; set; }
        public string NoteBodyTypeCode { get; set; }
        public List<string> Body { get; set; }
    }
}
