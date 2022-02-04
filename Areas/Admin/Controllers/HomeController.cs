using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DataManager dataManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(DataManager dataManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.dataManager = dataManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if(!User.IsInRole("admin"))
            {
                return NotFound();
            }

            return View(userManager.Users.ToList());
        }

        public IActionResult Articles()
        {
            return View(dataManager.Articles.GetArticles());
        }

        public IActionResult Categoryes()
        {
            return View(dataManager.Categoryes.GetCategoryes());
        }

        public IActionResult Comments()
        {
            return View(dataManager.Comments.GetComments());
        }
    }
}
