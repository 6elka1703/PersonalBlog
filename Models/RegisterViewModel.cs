using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Заполните имя пользователя")]
        [Display(Name = "Имя пользователя")]
        //[Remote(action: "VerifyUserName", controller: "AccountController", ErrorMessage = "Такое имя уже используется")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните Email")]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        [Display(Name = "Email")]
        //[Remote(action: "VerifyEmail", controller: "AccountController", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль должен содержать как минимум 6 символов", MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Заполните пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
