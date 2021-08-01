using Microsoft.AspNetCore.Mvc;
using NotesAPI.Interfaces;

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

        [HttpGet]
        public ActionResult<bool> Get()
        {
            _noteService.InsertNote();

            return true;
        }
    }
}
