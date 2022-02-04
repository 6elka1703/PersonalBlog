using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface IArticles
    {
        IList<Article> GetArticles();
        IList<Article> GetArticlesByIdList(List<int> listId);
        IList<Article> GetArticlesByCategoryId(int id);
        Article GetArticleById(int Id);
        void SaveArticle(Article article);
        void DeleteArticleById(int Id);
    }
}

