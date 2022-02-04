using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;
using Microsoft.Data.SqlClient;

namespace PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels
{
    public class Comments : IComments
    {
        private readonly ApplicationDbContext context;

        public Comments(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IList<Comment> GetComments()
        {
            return context.Comments.Include(x => x.Article).Include(x => x.Author).ToList();
        }

        public IList<Comment> GetCommentsByArticleId(int articleId)
        {
            return context.Comments.Include(x => x.Author).Where(p => p.ArticleId == articleId).OrderBy(p => p.Id).ToList();
        }

        public IList<Comment> GetCommentsByAuthor(ApplicationUser author)
        {
            return context.Comments.Include(x => x.Article)
                                   .Include(x => x.Author)
                                   .Where(x => x.Author == author)
                                   .ToList();
        }

        public Comment GetCommentById(int Id)
        {
            return context.Comments.Include(x => x.Author).FirstOrDefault(p => p.Id == Id);
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
