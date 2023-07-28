using System;
using System.Collections.Generic;
using System.Text;
using ToCook.Core.DTOs;
using ToCook.DataLayer.Entities.User;

namespace ToCook.Core.Services.Interfaces
{
   public interface IUserService
   {
       bool IsExistUserName(string userName);
       bool IsExistEmail(string email);
        public int AddUser(User user);
        public User LoginUser(LoginViewModel login);
        public bool ActiveAccount(string activeCode);
    }
}
