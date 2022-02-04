using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels
{
    public class Articles : IArticles
    {
        private readonly ApplicationDbContext context;

        public Articles(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IList<Article> GetArticles()
        {
            return context.Articles.Include(x => x.Category).ToList();
        }

        public Article GetArticleById(int Id)
        {
            return context.Articles.Include(x => x.Category)
                .FirstOrDefault(p => p.Id == Id);
        }

        public IList<Article> GetArticlesByIdList(List<int> listId)
        {
            return context.Articles.Include(x => x.Category).Where(p => listId.Contains(p.Id)).ToList();  
        }

        public IList<Article> GetArticlesByCategoryId(int id)
        {
            return context.Articles.Include(x => x.Category).Where(p => p.CategoryId == id).ToList();
        }

        public void SaveArticle(Article article)
        {
            if(article.Id == default(int))
            {
                article.DateAdd = DateTime.UtcNow;
                context.Entry(article).State = EntityState.Added;
            }
            else
            {
                context.Entry(article).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteArticleById(int Id)
        {
            if (Id != null)
            {
                Article article = GetArticleById(Id);
                if (article != null)
                {
                    context.Articles.Remove(article);
                    context.SaveChanges();
                }
            }
        }

    }
}
