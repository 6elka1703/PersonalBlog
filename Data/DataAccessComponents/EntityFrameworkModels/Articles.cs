using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;
using PersonalBlog.Models;


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
            return context.Articles.Include(x => x.Category)
                                   .Include(x => x.Author)
                                   .OrderByDescending(x => x.DateAdd)
                                   .ToList();
        }

        public IList<Article> GetArticlesByAuthor(ApplicationUser author)
        {
            return context.Articles.Include(x => x.Category)
                                   .Include(x => x.Author)
                                   .Where(x => x.Author == author)
                                   .OrderByDescending(x => x.DateAdd)
                                   .ToList();
        }

        public Article GetArticleById(int Id)
        {
            return context.Articles.Include(x => x.Category)
                .Include(x => x.Author)
                .FirstOrDefault(p => p.Id == Id);
        }

        public IList<Article> GetArticlesByIdList(List<int> listId)
        {
            return context.Articles.Include(x => x.Category)
                                    .Include(x => x.Author)
                                    .Where(p => listId.Contains(p.Id))
                                    .OrderByDescending(x => x.DateAdd).ToList();  
        }

        public IList<Article> GetArticlesByCategoryId(int id)
        {
            return context.Articles.Include(x => x.Category)
                                    .Include(x => x.Author)
                                    .Where(p => p.CategoryId == id)
                                    .OrderByDescending(x => x.DateAdd).ToList();
        }

        public IList<Article> GetArticlesByArchive(DateTime dateArchive)
        {
            DateTime dateStart = new DateTime(dateArchive.Year, dateArchive.Month, 1);
            DateTime dateEnd = new DateTime(dateArchive.Year, dateArchive.Month + 1, 1).AddDays(-1);

            return context.Articles.Include(x => x.Category)
                                    .Include(x => x.Author)
                                    .Where(p => p.DateAdd >= dateStart && p.DateAdd <= dateEnd)
                                    .OrderByDescending(x => x.DateAdd).ToList();
        }

        public IList<ArchivesViewModel> GetArchives()
        {
            var archives = context.Articles
                  .GroupBy(i => new { i.DateAdd.Year, i.DateAdd.Month})
                  .Select(g => new ArchivesViewModel
                  {
                      dateArchives = new DateTime(g.Key.Year, g.Key.Month, 1),
                      viewArchives = $"{new DateTime(g.Key.Year, g.Key.Month, 1).ToString("Y")}"
                  }).ToList();

            return archives;
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
