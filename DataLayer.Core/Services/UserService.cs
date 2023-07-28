using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToCook.Core.Convertors;
using ToCook.Core.DTOs;
using ToCook.Core.Generators;
using ToCook.Core.Security;
using ToCook.Core.Services.Interfaces;
using ToCook.DataLayer.Context;
using ToCook.DataLayer.Entities.User;

namespace ToCook.Core.Services
{
        public class UserService : IUserService
        {
            private ToCookContext _context;

            public UserService(ToCookContext context)
            {
                _context = context;
            }

            public bool ActiveAccount(string activeCode)
            {
                User user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);

                if (user == null || user.IsActive)
                {
                    return false;
                }

                user.IsActive = true;
                user.ActiveCode = NameGenerator.GenerateUniqueCode();
                _context.SaveChanges();
                return true;
            }

            public int AddUser(User user)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return user.UserId;
            }

            public bool IsExistEmail(string email)
            {
                return _context.Users.Any(u => u.Email == email);
            }

            public bool IsExistUserName(string userName)
            {
                return _context.Users.Any(u => u.UserName == userName);
            }

            public User LoginUser(LoginViewModel login)
            {
                string hashedPassword = PasswordHelper.EncodePasswordMd5(login.Password);
                string fixedEmail = FixedText.FixEmail(login.Email);
                return _context.Users.SingleOrDefault(u => u.Password == hashedPassword && u.Email == fixedEmail);
            }
        }
    }
