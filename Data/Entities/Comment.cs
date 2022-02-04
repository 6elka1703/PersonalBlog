using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Текст комментария не заполнен!")]
        [Display(Name = "Текст комментария")]
        public string CommentText { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата добавления")]
        public DateTime DateAdd { get; set; }

        [Required(ErrorMessage = "Автор комментария не заполнен!")]
        [Display(Name = "Автор комментария")]
        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int IdParentComment { get; set; }

        public Comment()
        {
            DateAdd = DateTime.UtcNow;
        }
    }
}
