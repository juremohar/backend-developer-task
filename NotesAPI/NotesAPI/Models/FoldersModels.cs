using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Models
{
    public class InsertFolderModel
    {
        public string Name { get; set; }
    }

    public class UpdateFolderModel
    {
        public string Name { get; set; }
    }

    public class FolderModel
    {
        public int IdFolder { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public DateTime InsertedAt { get; set; }
    }

    public class GetFoldersFilterModel
    {
        public string Query { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
