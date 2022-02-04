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

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private const int PAGE_SIZE = 5;

        private readonly DataManager dataManager;
        
        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
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

        private IndexViewModel ViewModel(IList<Article> data, int page = 1)
        {
            var countArticles = data.Count();
            var items = data.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            PageViewModel pageViewModel = new PageViewModel(countArticles, page, PAGE_SIZE);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                Articles = items,
                PageViewModel = pageViewModel
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
