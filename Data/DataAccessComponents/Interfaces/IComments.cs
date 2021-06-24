using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface IComments
    {
        IQueryable<Comment> GetComments();
        IQueryable<Comment> GetCommentsBySelectionField(string SelectionField, object value);
        Comment GetCommentById(int Id);
        void SaveComment(Comment comment);
        void DeleteCommentById(int Id);
    }
}
