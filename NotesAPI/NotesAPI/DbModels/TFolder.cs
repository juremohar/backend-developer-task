using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.DbModels
{
    public class TFolder
    {
        [Key]
        public int IdFolder { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
        
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertedAt { get; set; }

        public TUser User { get; set; }
    }
}
