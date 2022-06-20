using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalBlog.Data;
using PersonalBlog.Data.Entities;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private const int PAGE_SIZE = 5;

        private readonly DataManager dataManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(DataManager dataManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.dataManager = dataManager;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public IActionResult Index(int page = 1)
        {
            var data = dataManager.Articles.GetArticles();
            return View(ViewModel(data, page));
        }

        public IActionResult ArticlesByCategory(int categoryID)
        {
            var data = dataManager.Articles.GetArticlesByCategoryId(categoryID);
            return View("Index", ViewModel(data));
        }

        public IActionResult ArticlesByTag(string tag)
        {
            var articlesId = dataManager.ArticleWithTags.GetArticlesIdByTagName(tag);
            var data = dataManager.Articles.GetArticlesByIdList(articlesId.ToList());

            return View("Index", ViewModel(data));
        }

        public async Task<IActionResult> ArticlesByAuthor(string authorId)
        {
            var author = await _userManager.FindByIdAsync(authorId);
            var data = dataManager.Articles.GetArticlesByAuthor(author);
            return View("Index", ViewModel(data));
        }

        public IActionResult ArticlesByArchive(string dateArchiveStr)
        {
            DateTime dateArchive = DateTime.Parse(dateArchiveStr);
            var data = dataManager.Articles.GetArticlesByArchive(dateArchive);
            return View("Index", ViewModel(data));
        }

        private IndexViewModel ViewModel(IList<Article> data, int page = 1)
        {
            var countArticles = data.Count();
            var items = data.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            PageViewModel pageViewModel = new PageViewModel(countArticles, page, PAGE_SIZE);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                Articles = items,
                PageViewModel = pageViewModel,
                Categories = dataManager.Categoryes.GetCategoryes(),
                Archives = dataManager.Articles.GetArchives()
        };

            return indexViewModel;       
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
