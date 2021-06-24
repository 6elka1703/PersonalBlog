using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Areas.Admin.Models
{
    public class EditViewModel
    {
        public Article Article { get; set; }
        public SelectList Categoryes { get; set; }
        public IQueryable<string> Tags { get; set; }

    }
}
