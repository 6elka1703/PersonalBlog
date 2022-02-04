using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Укажите Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
