using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TNoteBody
    {
        [Key]
        public int IdNoteBody { get; set; }

        [ForeignKey("Note")]
        public int IdNote { get; set; }

        public string Body { get; set; }

        public TNote Note { get; set; }
    }
}
