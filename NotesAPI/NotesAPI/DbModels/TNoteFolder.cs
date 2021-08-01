using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TNoteFolder
    {
        [ForeignKey("Note")]
        public int IdNote { get; set; }

        [ForeignKey("Folder")]
        public int IdFolder { get; set; }

        public TNote Note { get; set; }

        public TFolder Folder { get; set; }
    }
}
