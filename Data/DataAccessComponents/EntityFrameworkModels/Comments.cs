using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels
{
    public class Comments : IComments
    {
        private readonly ApplicationDbContext context;

        public Comments(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Comment> GetComments()
        {
            return context.Comments.Include(x => x.Article);
        }

        public IQueryable<Comment> GetCommentsBySelectionField(string SelectionField, object value)
        {
            return context.Comments.FromSqlRaw("SELECT * FROM Comments WHERE " + SelectionField + " = " + value);
        }

        public Comment GetCommentById(int Id)
        {
            return context.Comments.FirstOrDefault(p => p.Id == Id);
        }

        public void SaveComment(Comment comment)
        {
            if (comment.Id == default(int))
            {
                context.Entry(comment).State = EntityState.Added;
            }
            else
            {
                context.Entry(comment).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void DeleteCommentById(int Id)
        {
            if (Id != null)
            {
                Comment comment = GetCommentById(Id);
                if (comment != null)
                {
                    context.Comments.Remove(comment);
                    context.SaveChanges();
                }
            }
        }   
    }
}
