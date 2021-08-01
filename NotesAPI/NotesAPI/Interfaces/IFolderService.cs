using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Interfaces
{
    public interface IFolderService
    {
        void InsertFolder(InsertFolderModel model);
        void UpdateFolder(int idFolder, UpdateFolderModel model);
        FolderModel GetFolderById(int idFolder);
        List<FolderModel> GetFolders(GetFoldersFilterModel model);
        void DeleteFolder(int idFolder);
    }
}
