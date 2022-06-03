using Microsoft.AspNetCore.Http;
using MyNoteSample.Core;
using MyNoteSample.Models;
using MyNoteSample.Models.Context;
using MyNoteSample.Models.Entities;
using System;

namespace MyNoteSample.Business
{
    public class NoteService
    {
        private MyNoteDbContext _myNoteDbContext = new MyNoteDbContext();

        public ServiceResult<Note> Create(NoteViewModel model, HttpContext httpContext)
        {
            ServiceResult<Note> result = new ServiceResult<Note>();



            Note note = new Note
            {
                Title = model.Title,
                Summary = model.Summary,
                Detail = model.Detail,
                IsDraft = model.IsDraft,
                CategoryId = model.CategoryId,
                UserId = httpContext.Session.GetInt32(Constants.UserId).Value,
                CreatedUser = httpContext.Session.GetString(Constants.UserName),
                CreatedAt = DateTime.Now
            };
            _myNoteDbContext.Notes.Add(note);

            if (_myNoteDbContext.SaveChanges() == 0)
            {
                result.AddError("Kayıt yapılamadı");
            }
            else
            {
                result.Data = note;
            }
            return result;
        }

    }
}
