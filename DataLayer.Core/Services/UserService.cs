using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToCook.Core.Services.Interfaces;
using ToCook.DataLayer.Context;

namespace ToCook.Core.Services
{
    public class UserService:IUserService
    {
        private ToCookContext _context;

        public UserService(ToCookContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
