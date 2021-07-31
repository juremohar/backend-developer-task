using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TNoteVisibility
    {
        [Key]
        public int IdVisibility { get; set; }

        public string Code { get; set; }
        
        public string Title { get; set; }
    }
}
