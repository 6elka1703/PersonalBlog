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
        public IQueryable<string> Tags { get; set; }
        public IQueryable<Comment> Comments { get; set; }

    }
}
