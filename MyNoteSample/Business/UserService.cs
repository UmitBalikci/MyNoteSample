using Microsoft.AspNetCore.Http;
using MyNoteSample.Core;
using MyNoteSample.Models;
using MyNoteSample.Models.Context;
using MyNoteSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNoteSample.Business
{
    public class UserService
    {
        private MyNoteDbContext _myNoteDbContext = new MyNoteDbContext();
        private HttpContext _httpContext;
        public UserService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }
        public ServiceResult<object> Register(RegisterViewModel model)
        {
            ServiceResult<object> result = new ServiceResult<object>();

            model.Username = model.Username.Trim().ToLower();

            if (_myNoteDbContext.Users.Any(m => m.Email.ToLower() == model.Email.ToLower()))
            {   
                result.AddError($"Bu '{model.Email}' adresi zeten kayıtlı");
                return result;
            }
            if (_myNoteDbContext.Users.Any(m => m.Username.ToLower() == model.Username.ToLower()))
            {
                result.AddError($"Bu '{model.Username}' zeten kayıtlı");
                return result;
            }
            _myNoteDbContext.Users.Add(new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                IsActive = true,
                IsAdmin = false,
                CreatedUser = "register",
                CreatedAt = DateTime.Now
            });

            if (_myNoteDbContext.SaveChanges() == 0)
            {
                result.AddError("Kayıt yapılamadı");
            }
            return result;
        }

        public ServiceResult<User> Login(LoginViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();

            model.Username = model.Username.Trim().ToLower();
            
            User user = _myNoteDbContext.Users.SingleOrDefault(
                u => u.Username.ToLower() == model.Username &&
                u.Password == model.Password);

            if (user == null)
            {
                result.AddError("Hatalı kullanıcı adı veya şifre");
            }
            else
            {
                user.Password = string.Empty;
                result.Data = user;
            }
            return result;
        }

        public ServiceResult<List<User>> List()
        {
            List<User> users = _myNoteDbContext.Users.ToList();

            users.ForEach(u => u.Password = string.Empty);

            ServiceResult<List<User>> result = new ServiceResult<List<User>>();
            result.Data = users;

            return result;
            
        }

        public ServiceResult<User> Create(UserViewModel model)
        {
            ServiceResult<User> result = new ServiceResult<User>();
            User user = new User
            {
                FullName = model.FullName,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                IsActive = model.IsActive,
                IsAdmin = model.IsAdmin,
                CreatedUser = _httpContext.Session.GetString(Constants.UserName),
                CreatedAt = DateTime.Now
            };
            _myNoteDbContext.Users.Add(user);

            if (_myNoteDbContext.SaveChanges() == 0)
            {
                result.AddError("Kayıt yapılamadı");
            }
            else
            {
                result.Data = user;
            }
            return result;
        }
    }
}
