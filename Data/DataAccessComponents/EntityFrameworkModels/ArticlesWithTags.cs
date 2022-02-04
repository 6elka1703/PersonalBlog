using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels
{
    public class ArticlesWithTags : IArticleWithTags
    {
        private readonly ApplicationDbContext context;

        public ArticlesWithTags(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IList<string> GetTagsByArticleId(int id)
        {
            return (from t in context.ArticleWithTags
                    where t.ArticleId == id
                    select t.TagName).Distinct().ToList();
        }

        public IList<int> GetArticlesIdByTagName(string tagName)
        {
            return context.ArticleWithTags
                .Where(x => x.TagName == tagName)
                .GroupBy(x => new { x.TagName, x.ArticleId })
                .Select(x => x.Key.ArticleId).Distinct().ToList();
        }

        public void SaveTags(List<ArticleWithTags> tags)
        {
            foreach (var tagObject in tags)
            {
                context.ArticleWithTags.Add(tagObject);
            }

            context.SaveChanges();
        }
    }
}
