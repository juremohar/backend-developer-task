using Microsoft.AspNetCore.Connections.Features;
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
    public class FolderService : IFolderService
    {
        private readonly DbNotes _db;
        private readonly IAuthService _authService;

        public FolderService
        (
            DbNotes db,
            IAuthService authService
        ) 
        {
            _db = db;
            _authService = authService;
        }

        public void DeleteFolder(int idFolder)
        {
            // here we need to decide if we delete all the notes, that the folder contains as well
            // I decided to only delete the folder

            var userDetails = _authService.GetUserDetails();

            if (userDetails == null)
            {
                throw new Exception("not logged in");
            }

            var folder = _db
                .Folders
                .Single(x => x.IdFolder == idFolder);

            if (folder.IdUser != userDetails.IdUser) 
            {
                throw new Exception("you have no permission for this folder");
            }

            var folderNotes = _db
                .NoteFolder
                .Where(x => x.IdFolder == idFolder);

            // TODO: optimization - mass delete
            foreach (var _folderNote in folderNotes) 
            {
                _db.NoteFolder.Remove(_folderNote);
            }

            _db.Folders.Remove(folder);
            _db.SaveChanges();
        }

        public FolderModel GetFolderById(int idFolder)
        {
            var userDetails = _authService.GetUserDetails();

            if (userDetails == null)
            {
                throw new Exception("not logged in");
            }

            var folder = _db
                .Folders
                .Where(x => x.IdFolder == idFolder)
                .Select(x => new FolderModel
                {
                    IdFolder = x.IdFolder,
                    IdUser = x.IdUser,
                    Name = x.Name,
                    InsertedAt = x.InsertedAt
                })
                .FirstOrDefault();

            if (folder == null) 
            {
                throw new Exception("no folder found");
            }

            if (folder.IdUser != userDetails.IdUser) 
            {
                throw new Exception("you cannot access foreign folders");
            }

            return folder;
        }

        public List<FolderModel> GetFolders(GetFoldersFilterModel model)
        {
            // first we validate data and throw errors if not okay
            var userDetails = _authService.GetUserDetails();

            if (userDetails == null)
            {
                throw new Exception("not logged in");
            }

            var folders = _db.Folders.AsQueryable();

            if (model.IdUser.HasValue) 
            {
                if (model.IdUser != userDetails.IdUser) 
                {
                    throw new Exception("you cannot access foreign folders");
                }

                folders = folders.Where(x => x.IdUser == model.IdUser);
            }

            if (!string.IsNullOrEmpty(model.Query)) 
            {
                model.Query = model.Query.ToLower();

                folders = folders.Where(x => x.Name.ToLower().Contains(model.Query));
            }

            folders = folders.OrderBy(x => x.IdUser).ThenBy(x => x.Name);

            // we take one more than the limit, so we can generate pagination (if result.count > limit, we show arrow for more pages)
            folders = folders.Skip((model.Page - 1) * model.Limit).Take(model.Limit + 1);

            var result = folders
                .Select(x => new FolderModel
                {
                    IdFolder = x.IdFolder,
                    IdUser = x.IdUser,
                    Name = x.Name,
                    InsertedAt = x.InsertedAt
                })
                .ToList();

            return result;
        }

        public void InsertFolder(InsertFolderModel model)
        {
            // first we validate data and throw errors if not okay
            var userDetails = _authService.GetUserDetails();

            if (userDetails == null) 
            {
                throw new Exception("not logged in");
            }

            if (string.IsNullOrEmpty(model.Name)) 
            {
                throw new Exception("folder name is missing");
            }

            TFolder folder = new()
            {
                IdUser = userDetails.IdUser,
                Name = model.Name
            };

            _db.Folders.Add(folder);
            _db.SaveChanges();
        }

        public void UpdateFolder(int idFolder, UpdateFolderModel model)
        {
            var userDetails = _authService.GetUserDetails();

            if (userDetails == null)
            {
                throw new Exception("not logged in");
            }

            var folder = _db.Folders.Single(x => x.IdFolder == idFolder);

            if (folder.IdUser != userDetails.IdUser) 
            {
                throw new Exception("you don't have permission to edit this folder");
            }

            folder.Name = model.Name;
            _db.SaveChanges();
        }
    }
}
