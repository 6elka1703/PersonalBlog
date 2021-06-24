using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface IArticles
    {
        IQueryable<Article> GetArticles();
        IQueryable<Article> GetArticlesBySelectionField(string SelectionField, object value);
        Article GetArticleById(int Id);
        void SaveArticle(Article article);
        void DeleteArticleById(int Id);
    }
}

