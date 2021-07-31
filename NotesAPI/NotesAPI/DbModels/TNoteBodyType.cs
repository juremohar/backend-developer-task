using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TNoteBodyType
    {
        [Key]
        public int IdNoteBodyType { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }
    }
}
