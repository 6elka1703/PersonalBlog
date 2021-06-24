using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace PersonalBlog.Data.Entities
{
    public class Article
    {
        private readonly UserManager<IdentityUser> _userManager;

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Заполните заголовок")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Заполните краткое содержание")]
        [Display(Name = "Краткое содержание")]
        public string ShortDescription { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата создания")]
        public DateTime DateAdd { get; }

        [Required(ErrorMessage = "Заполните автора")]
        [Display(Name = "Автор")]
        public String Author { get; set; }

        [Display(Name = "Титутульная картинка")]
        public String TitleImagePath { get; set; }

        [Required(ErrorMessage = "Заполните сожержание статьи")]
        [Display(Name = "Содержание")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Заполните категорию")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Article()
        {
            DateAdd = DateTime.UtcNow;  
           
        }

    }
}
