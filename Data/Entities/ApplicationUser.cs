using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string PhotoPath { get; set; }

        public string GetPhotoPath()
        {
            return PhotoPath == null ? "upload/UserPhotoEmpty.jpg" : PhotoPath;
        }
    }
}
