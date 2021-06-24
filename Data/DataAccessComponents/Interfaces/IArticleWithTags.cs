using PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels;
using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface IArticleWithTags
    {
        IQueryable<string> GetTagsByArticleId(int id);
        List<int> GetArticlesIdByTagName(string tagName);

        void SaveTags(List<ArticleWithTags> tags);

    }
}
