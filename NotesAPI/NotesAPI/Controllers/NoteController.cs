using Microsoft.AspNetCore.Mvc;
using NotesAPI.Interfaces;
using NotesAPI.Models;
using System.Collections.Generic;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController
        (
            INoteService noteService
        )
        {
            _noteService = noteService;
        }

        [HttpPost]
        public ActionResult<bool> Insert(InsertNoteModel model)
        {
            _noteService.InsertNote(model);

            return true;
        }

        [HttpPatch("{id}")]
        public ActionResult<bool> Update(int id, UpdateNoteModel model)
        {
            _noteService.UpdateNote(id, model);

            return true;
        }

        [HttpGet]
        public ActionResult<List<NoteModel>> GetNotes([FromQuery] GetNotesFilterModel model)
        {
            return _noteService.GetNotes(model);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteModel> GetNoteById(int id)
        {
            return _noteService.GetNoteById(id);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _noteService.DeleteNote(id);

            return true;
        }
    }
}
