using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Models
{
    public class InsertNoteModel 
    {
        public int? IdFolder { get; set; }
        [Description("public or private")]
        public string VisibilityCode { get; set; }
        public string Title { get; set; }
        [Description("single or private")]
        public string NoteBodyTypeCode { get; set; }
        public List<string> Body { get; set; }
    }

    public class UpdateNoteModel 
    {
        [Description("public or private")]
        public string VisibilityCode { get; set; }
        public string Title { get; set; }
        [Description("single or multiple")]
        public string NoteBodyTypeCode { get; set; }
        public List<string> Body { get; set; }
        public List<int> Folders { get; set; }
    }

    public class GetNotesFilterModel 
    {
        public int? IdFolder { get; set; }
        [Description("public or private")]
        public string VisibilityCode { get; set; }
        public string Query { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        [Description("visibility or heading")]
        public string OrderByField { get; set; }
        [Description("asc or desc")]
        public string OrderByDirection { get; set; }
    }

    public class NoteModel 
    {
        public int IdNote { get; set; }
        public int IdUser { get; set; }
        public NoteVisibilityModel Visibility { get; set; }
        public NoteBodyTypeModel BodyType { get; set; }
        public string Title { get; set; }
        public DateTime InsertedAt { get; set; }
        public List<string> BodyList { get; set; }
    }

    public class NoteVisibilityModel
    {
        public int IdNoteVisibility { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class NoteBodyTypeModel 
    {
        public int IdNoteBodyType { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class NoteVisibilityCodesConstants
    {
        public const string Public = "public";
        public const string Private = "private";
    }

    public class OrderByConstants
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }
}
