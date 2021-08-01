using Microsoft.EntityFrameworkCore;
using NotesAPI.DbModels;
using NotesAPI.Interfaces;
using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var folder = _db.Folders.Single(x => x.IdFolder == model.IdFolder && x.IdUser == details.IdUser);
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
                var entities = new List<TNoteBody>();

                foreach (var _body in model.Body) 
                {
                    entities.Add(new() 
                    {
                        IdNote = note.IdNote,
                        Body = _body
                    });
                }

                _db.NoteBody.AddRange(entities);
                _db.SaveChanges();
            }
        }

        public void DeleteNote(int idNote) 
        {
            // here we need to decide, if we will do soft delete or hard delete
            // I went with hard delete

            var userDetails = _authService.GetUserDetails();

            if (userDetails == null) 
            {
                throw new Exception("not logged in");
            }

            var note = _db
                .Notes
                .Single(x => x.IdNote == idNote);

            if (userDetails.IdUser != note.IdUser) 
            {
                throw new Exception("no permission to delete note");
            }

            var noteBodies = _db.NoteBody.Where(x => x.IdNote == idNote);
            _db.NoteBody.RemoveRange(noteBodies);
            
            _db.Notes.Remove(note);
            _db.SaveChanges();
        }

        public void UpdateNote(int idNote, UpdateNoteModel model)
        {
            var userDetails = _authService.GetUserDetails();

            if (userDetails == null)
            {
                throw new Exception("not logged in");
            }

            var note = _db.Notes.Single(x => x.IdNote == idNote);

            if (userDetails.IdUser != note.IdUser) 
            {
                throw new Exception("no permission to edit this note");
            }

            if (model.Folders != null)
            {
                // remove all old records
                var noteFolders = _db.NoteFolder.Where(x => x.IdNote == idNote);
                _db.NoteFolder.RemoveRange(noteFolders);

                if (model.Folders.Count() > 0) 
                {
                    // add all new ones
                    var folders = _db.Folders.Where(x => model.Folders.Contains(x.IdFolder) && x.IdUser == userDetails.IdUser);

                    if (model.Folders.Count() != folders.Count())
                    {
                        throw new Exception("some folders do not exists in db");
                    }

                    var entities = new List<TNoteFolder>();

                    foreach (var _folder in folders) 
                    {
                        entities.Add(new TNoteFolder
                        {
                            IdFolder = _folder.IdFolder,
                            IdNote = idNote
                        });
                    }

                    _db.NoteFolder.AddRange(entities);
                }
            }


            if (!string.IsNullOrEmpty(model.VisibilityCode)) 
            {
                var visibility = _db.NoteVisibilities.FirstOrDefault(x => x.Code == model.VisibilityCode);

                if (visibility == null) 
                {
                    throw new Exception("no visibility with this code in db");
                }

                note.IdNoteVisibility = visibility.IdVisibility;
            }

            if (!string.IsNullOrEmpty(model.Title)) 
            {
                note.Title = model.Title; 
            }

            if (!string.IsNullOrEmpty(model.NoteBodyTypeCode))
            {
                var type = _db.NoteBodyType.FirstOrDefault(x => x.Code == model.NoteBodyTypeCode);

                if (type == null)
                {
                    throw new Exception("no type with this code in db");
                }

                note.IdNoteBodyType = type.IdNoteBodyType;
            }

            if (model.Body != null) 
            {
                // remove all old records
                var noteBodies = _db.NoteBody.Where(x => x.IdNote == idNote);
                _db.NoteBody.RemoveRange(noteBodies);

                if (model.Body.Count() > 0)
                {
                    // add all new ones
                    var entities = new List<TNoteBody>();

                    foreach (var _body in model.Body)
                    {
                        entities.Add(new TNoteBody
                        {
                            IdNote = idNote,
                            Body = _body
                        });
                    }

                    _db.NoteBody.AddRange(entities);
                }
            }

            _db.SaveChanges();
        }

        public NoteModel GetNoteById(int idNote)
        {
            var userDetails = _authService.GetUserDetails();

            var note = _db
                .Notes
                .Include(x => x.NoteVisibility)
                .Include(x => x.NoteBodyType)
                .Include(x => x.NoteBodies)
                .FirstOrDefault(x => x.IdNote == idNote);

            if (note == null) 
            {
                throw new Exception("no note");
            }

            if (note.NoteVisibility.Code == NoteVisibilityCodesConstants.Private && userDetails == null) 
            {
                throw new Exception("no permission to see this");
            }

            if (note.NoteVisibility.Code == NoteVisibilityCodesConstants.Private && userDetails != null && note.IdUser != userDetails.IdUser) 
            {
                throw new Exception("no permission to see this");
            }

            return new NoteModel
            {
                IdNote = note.IdNote,
                IdUser = note.IdUser,
                Visibility = new NoteVisibilityModel 
                {
                    IdNoteVisibility = note.IdNoteVisibility,
                    Code = note.NoteVisibility.Code,
                    Title = note.NoteVisibility.Title
                },
                BodyType = new NoteBodyTypeModel 
                {
                    IdNoteBodyType = note.IdNoteBodyType,
                    Code = note.NoteBodyType.Code,
                    Title = note.NoteBodyType.Title,
                },
                Title = note.Title,
                InsertedAt = note.InsertedAt,
                BodyList = note.NoteBodies != null ? note.NoteBodies.Select(x => x.Body).ToList() : null
            };
        }

        public List<NoteModel> GetNotes(GetNotesFilterModel model)
        {
            var userDetails = _authService.GetUserDetails();

            var notes = _db
                .Notes
                .Include(x => x.NoteBodies)
                .Include(x => x.NoteVisibility)
                .Include(x => x.NoteBodyType)
                .Include(x => x.Folders)
                .AsQueryable();

            if (userDetails == null) 
            {
                notes = notes.Where(x => x.NoteVisibility.Code == NoteVisibilityCodesConstants.Public);
            }
            else 
            {
                notes = notes.Where(x => x.IdUser == userDetails.IdUser);
            }

            if (!string.IsNullOrEmpty(model.VisibilityCode)) 
            {
                var visibility = _db.NoteVisibilities.FirstOrDefault(x => x.Code == model.VisibilityCode);

                if (visibility == null) 
                {
                    throw new Exception("no visibility in db");
                }

                notes = notes.Where(x => x.IdNoteVisibility == visibility.IdVisibility);
            }

            if (model.IdFolder.HasValue) 
            {
                var folder = _db.Folders.FirstOrDefault(x => x.IdFolder == model.IdFolder);

                if (folder == null)
                {
                    throw new Exception("no folder in db");
                }

                notes = notes.Where(x => x.Folders.Any(y => y.IdFolder == model.IdFolder));
            }

            if (!string.IsNullOrEmpty(model.Query))
            {
                model.Query = model.Query.ToLower();

                notes = notes.Where(x => x.NoteBodies.Any(y => y.Body.ToLower().Contains(model.Query)));
            }

            if (!string.IsNullOrEmpty(model.OrderByField)) 
            {
                if (model.OrderByDirection.ToLower() == OrderByConstants.Ascending) 
                {
                    switch (model.OrderByField)
                    {
                        case "visibility": notes = notes.OrderBy(x => x.IdNoteVisibility).ThenBy(x => x.IdNote); break;
                        case "heading": notes = notes.OrderBy(x => x.Title); break;
                    }
                }
                else 
                {
                    switch (model.OrderByField)
                    {
                        case "visibility": notes = notes.OrderByDescending(x => x.IdNoteVisibility).ThenBy(x => x.IdNote); break;
                        case "heading": notes = notes.OrderByDescending(x => x.Title); break;
                    }
                }
            }

            // take one more for pagination ...
            notes = notes.Skip((model.Page - 1) * model.Limit).Take(model.Limit + 1);

            var result = notes
                .Select(x => new NoteModel
                {
                    IdNote = x.IdNote,
                    IdUser = x.IdUser,
                    Visibility = new NoteVisibilityModel
                    {
                        IdNoteVisibility = x.IdNoteVisibility,
                        Code = x.NoteVisibility.Code,
                        Title = x.NoteVisibility.Title
                    },
                    BodyType = new NoteBodyTypeModel
                    {
                        IdNoteBodyType = x.IdNoteBodyType,
                        Code = x.NoteBodyType.Code,
                        Title = x.NoteBodyType.Title,
                    },
                    Title = x.Title,
                    InsertedAt = x.InsertedAt,
                    BodyList = x.NoteBodies != null ? x.NoteBodies.Select(x => x.Body).ToList() : null
                })
                .ToList();

            return result;
        }
    }
}
