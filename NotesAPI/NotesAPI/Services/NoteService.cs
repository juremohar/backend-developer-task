using Microsoft.EntityFrameworkCore;
using NotesAPI.DbModels;
using NotesAPI.Interfaces;
using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NotesAPI.Services
{
    public class NoteService : INoteService
    {
        private readonly DbNotes _db;
        private readonly IAuthService _authService;

        public NoteService
        (
            DbNotes db,
            IAuthService authService
        ) 
        {
            _db = db;
            _authService = authService;
        }

        public void InsertNote(InsertNoteModel model) 
        {
            var details = _authService.GetUserDetails();

            if (details == null) 
            {
                throw new Exception("not logged in");
            }

            if (string.IsNullOrEmpty(model.Title)) 
            {
                throw new Exception("missing title");
            }

            if (string.IsNullOrEmpty(model.VisibilityCode)) 
            {
                throw new Exception("missing visibility code");
            }

            if (string.IsNullOrEmpty(model.NoteBodyTypeCode)) 
            {
                throw new Exception("missing note body type");
            }

            if (model.IdFolder.HasValue) 
            {
                var folder = _db.Folders.FirstOrDefault(x => x.IdFolder == model.IdFolder);
                if (folder == null) 
                {
                    throw new Exception("folder doesn't exist");
                }
            }

            var visibilityCode = _db
                .NoteVisibilities
                .Single(x => x.Code.ToLower() == model.VisibilityCode);

            var noteBodyType = _db
                .NoteBodyType
                .Single(x => x.Code.ToLower() == model.NoteBodyTypeCode);

            TNote note = new()
            {
                IdUser = _authService.GetUserDetails().IdUser,
                IdNoteVisibility = visibilityCode.IdVisibility,
                IdNoteBodyType = noteBodyType.IdNoteBodyType,
                Title = model.Title
            };

            _db.Notes.Add(note);
            _db.SaveChanges();

            if (model.IdFolder.HasValue) 
            {
                var folder = _db.Folders.Single(x => x.IdFolder == model.IdFolder);

                TNoteFolder noteFolder = new()
                {
                    IdFolder = folder.IdFolder,
                    IdNote = note.IdNote
                };

                _db.NoteFolder.Add(noteFolder);
                _db.SaveChanges();
            }

            if (model.Body != null && model.Body.Count() > 0) 
            {
                // optimize for mass insert
                foreach (var _body in model.Body) 
                {
                    TNoteBody noteBodyRow = new()
                    {
                        IdNote = note.IdNote,
                        Body = _body
                    };

                    _db.NoteBody.Add(noteBodyRow);
                    _db.SaveChanges();
                }
            }
        }

        public void DeleteNote(int idNote) 
        {
            // here we need to decide, if we will do soft delete or hard delete
            // I went with hard delete

            var note = _db
                .Notes
                .Single(x => x.IdNote == idNote);

            var noteBodies = _db.NoteBody.Where(x => x.IdNote == idNote);

            // TODO: optimization (mass remove)
            foreach (var _body in noteBodies)
            {
                _db.NoteBody.Remove(_body);
            }

            _db.Notes.Remove(note);
            _db.SaveChanges();
        }
    }
}
