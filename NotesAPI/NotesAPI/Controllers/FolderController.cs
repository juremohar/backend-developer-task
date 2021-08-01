using Microsoft.AspNetCore.Mvc;
using NotesAPI.Interfaces;
using NotesAPI.Models;
using System.Collections.Generic;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController
        (
            IFolderService folderService
        )
        {
            _folderService = folderService;
        }

        [HttpPost]
        public ActionResult<bool> Insert(InsertFolderModel model)
        {
            _folderService.InsertFolder(model);

            return true;
        }

        [HttpPatch("{id}")]
        public ActionResult<bool> Update(int id, UpdateFolderModel model)
        {
            _folderService.UpdateFolder(id, model);

            return true;
        }

        [HttpGet]
        public ActionResult<List<FolderModel>> GetFolders([FromQuery] GetFoldersFilterModel model)
        {
            return _folderService.GetFolders(model);
        }

        [HttpGet("{id}")]
        public ActionResult<FolderModel> GetFolderById(int id)
        {
            return _folderService.GetFolderById(id);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _folderService.DeleteFolder(id);

            return true;
        }
    }
}
