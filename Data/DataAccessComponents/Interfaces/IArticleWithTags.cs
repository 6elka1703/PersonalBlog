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
        IList<string> GetTagsByArticleId(int id);
        IList<int> GetArticlesIdByTagName(string tagName);

        void SaveTags(List<ArticleWithTags> tags);

    }
}
