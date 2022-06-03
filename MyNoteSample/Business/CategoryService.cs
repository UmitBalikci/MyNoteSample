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
    public class CategoryService
    {
        private MyNoteDbContext _myNoteDbContext = new MyNoteDbContext();

        public ServiceResult<List<Category>> List()
        {
            List<Category> categories = _myNoteDbContext.Categories.ToList();

            ServiceResult<List<Category>> result = new ServiceResult<List<Category>>();
            result.Data = categories;

            return result;

        }

        public ServiceResult<Category> Create(CategoryViewModel model, HttpContext httpContext)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();

            model.Name = model.Name.Trim();

            if (_myNoteDbContext.Categories.Any(m => m.Name.ToLower() == model.Name.ToLower()))
            {
                result.AddError($"Bu '{model.Name}' kategorisi zeten var");
                return result;
            }

            Category category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                CreatedUser = httpContext.Session.GetString(Constants.UserName),
                CreatedAt = DateTime.Now
            };
            _myNoteDbContext.Categories.Add(category);

            if (_myNoteDbContext.SaveChanges() == 0)
            {
                result.AddError("Kayıt yapılamadı");
            }
            else
            {
                result.Data = category;
            }
            return result;
        }

        public ServiceResult<Category> Find(int id)
        {
            ServiceResult<Category> result = new ServiceResult<Category>()
            {
                Data = _myNoteDbContext.Categories.Find(id)
            };

            if (result.Data == null)
            {
                result.AddError("Kayıt Bulunamadı");
            }

            return result;
        }

        public ServiceResult<Category> Update(int id, CategoryViewModel model, HttpContext httpContext)
        {
            ServiceResult<Category> result = new ServiceResult<Category>();

            model.Name = model.Name.Trim();
            

            if (_myNoteDbContext.Categories.Any(m => m.Name.ToLower() == model.Name.ToLower() && m.Id != id))
            {
                result.AddError($"Bu '{model.Name}' isimli kategori zeten kayıtlı");
                return result;
            }
            
            Category category = _myNoteDbContext.Categories.Find(id);

            category.Name = model.Name;
            category.Description = model.Description;
            category.ModifiedUser = httpContext.Session.GetString(Constants.UserName);
            category.ModifiedAt = DateTime.Now;

            if (_myNoteDbContext.SaveChanges() == 0)
            {
                result.AddError("Güncelleme yapılamadı");
            }
            else
            {
                result.Data = category;
            }

            return result;
        }

        public ServiceResult<object> Remove(int id)
        {
            ServiceResult<object> result = new ServiceResult<object>();

            Category category = _myNoteDbContext.Categories.Find(id);
            if (category != null)
            {
                _myNoteDbContext.Categories.Remove(category);
                if (_myNoteDbContext.SaveChanges() == 0)
                {
                    result.AddError("Silme işlemi yapılamadı");
                }
            }
            return result;
        }
    }
}
