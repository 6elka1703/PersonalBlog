using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface IComments
    {
        IList<Comment> GetComments();
        IList<Comment> GetCommentsByArticleId(int articleId);
        IList<Comment> GetCommentsByAuthor(ApplicationUser author);
        Comment GetCommentById(int Id);
        void SaveComment(Comment comment);
        void DeleteCommentById(int Id);
    }
}
