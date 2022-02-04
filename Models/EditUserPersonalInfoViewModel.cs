using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Models
{
    public class EditUserPersonalInfoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }

    }
}
