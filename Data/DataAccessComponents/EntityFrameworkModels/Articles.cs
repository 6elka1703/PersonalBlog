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

        public IQueryable<Article> GetArticles()
        {
            return context.Articles.Include(x => x.Category);
        }

        public Article GetArticleById(int Id)
        {
            return context.Articles.Include(x => x.Category)
                .FirstOrDefault(p => p.Id == Id);
        }

        public IQueryable<Article> GetArticlesBySelectionField(string SelectionField, object value)
        {
            if(value.GetType() == typeof(List<int>))
            {
                return context.Articles
                        .FromSqlRaw("SELECT * FROM Articles WHERE " + SelectionField + "in(" + value.ToString() + ")")
                        .Include(x => x.Category);
            }
            else
            {
                return context.Articles
                        .FromSqlRaw("SELECT * FROM Articles WHERE " + SelectionField + " = " + value)
                        .Include(x => x.Category);
            }
            
           
        }

        public void SaveArticle(Article article)
        {
            if(article.Id == default(int))
            {
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
