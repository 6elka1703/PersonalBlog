using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование категории не заполнено!")]
        [Display(Name = "Наименование категории")]
        public string Name { get; set; }

        public List<Article> Articles { get; set; }
    }
}
