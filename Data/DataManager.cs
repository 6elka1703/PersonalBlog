using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.DataAccessComponents.Interfaces;

namespace PersonalBlog.Data
{
    public class DataManager
    {
        public IArticles Articles { get; set; }
        public ICategoryes Categoryes { get; set; }
        public IComments Comments { get; set; }
        public IArticleWithTags ArticleWithTags { get; set; }


        public DataManager(IArticles articles, ICategoryes categoryes, IComments comments, IArticleWithTags articleWithTags)
        {
            Articles = articles;
            Categoryes = categoryes;
            Comments = comments;
            ArticleWithTags = articleWithTags;
        }

    }
}
