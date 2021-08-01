using Microsoft.AspNetCore.Mvc;
using NotesAPI.Interfaces;
using NotesAPI.Models;

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

        [HttpDelete("{idNote}")]
        public ActionResult<bool> Delete(int idNote)
        {
            _noteService.DeleteNote(idNote);

            return true;
        }
    }
}
