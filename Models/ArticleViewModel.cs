using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Models
{
    public class ArticleViewModel
    {
    
        public Article Article { get; set; }
        public IList<string> Tags { get; set; }
        public IList<Comment> Comments { get; set; }

    }
}
