using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Data.Entities
{
    public class ArticleWithTags
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
