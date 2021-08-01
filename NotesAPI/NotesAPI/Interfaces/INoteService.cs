using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Interfaces
{
    public interface INoteService
    {
        void InsertNote(InsertNoteModel model);
        void DeleteNote(int idNote);
        void UpdateNote(int idNote, UpdateNoteModel model);
        NoteModel GetNoteById(int idNote);
        List<NoteModel> GetNotes(GetNotesFilterModel model);
    }
}
