using Microsoft.AspNetCore.Http;
using NotesAPI.DbModels;
using NotesAPI.Interfaces;
using NotesAPI.Models;
using System;
using System.Linq;
using System.Text;

namespace NotesAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbNotes _db;
        private LoggedInUserModel _userDetails = null;

        public LoggedInUserModel GetUserDetails() 
        {
            return _userDetails;
        }

        public AuthService
        (
            IHttpContextAccessor httpContextAccessor, 
            DbNotes db
        )
        {
            _db = db;

            var context = httpContextAccessor.HttpContext;

            string token = null;

            if (context.Request.Headers.TryGetValue("Authorization", out var content))
            {
                token = content.ToString().Substring("Basic ".Length);
            }

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(token));

            var split = usernamePassword.Split(':');

            var userDetails = _db
                .Users
                .Where(x => x.Username.ToLower() == split.First().ToLower() && x.Password == split.Last())
                .Select(x => new LoggedInUserModel
                {
                    IdUser = x.IdUser,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username
                })
                .FirstOrDefault();

            if (userDetails != null) 
            {
                _userDetails = userDetails;
            } 
        }
    }
}
