using NotesAPI.DbModels;
using NotesAPI.Interfaces;
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

        public void InsertNote() 
        {
            Console.WriteLine("insert");

            var details = _authService.GetUserDetails();

            if (details == null) 
            {
                throw new Exception("not logged in");
            }
        }
    }
}
