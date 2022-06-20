using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalBlog.Areas.User.Models;
using PersonalBlog.Data;
using PersonalBlog.Data.Entities;
using PersonalBlog.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleController(DataManager dataManager, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = id == default ? new Article() : dataManager.Articles.GetArticleById(id);

            var categoryes = dataManager.Categoryes.GetCategoryes();
            var selectCategoryes = new SelectList(categoryes, "Id", "Name");
            var tags = dataManager.ArticleWithTags.GetTagsByArticleId(id);

            EditViewModel viewModel = new EditViewModel
            {
                Article = article,
                Categoryes = selectCategoryes,
                Tags = tags
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Article article, IFormFile titleImageFile, string tagsStr)
        {
            if(String.IsNullOrEmpty(article.AuthorId))
            {
                article.AuthorId = userManager.GetUserId(User);
            }

            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    StaticFilesService staticFilesService = new StaticFilesService(hostingEnvironment);
                    article.TitleImagePath = staticFilesService.SaveFile(titleImageFile, "images/");
                }
                dataManager.Articles.SaveArticle(article);

                var tags = tagsStr.Split(",");

                List<ArticleWithTags> tagList = new List<ArticleWithTags>();
                foreach (var tag in tags)
                {
                    var articleWithTags = new ArticleWithTags
                    {
                        ArticleId = article.Id,
                        TagName = tag
                    };

                    tagList.Add(articleWithTags);
                }

                dataManager.ArticleWithTags.SaveTags(tagList);

                return RedirectToAction("Articles", "Home");
            }
            return View(article);
        }

        public IActionResult Delete(int id)
        {
            dataManager.Articles.DeleteArticleById(id);
            return RedirectToAction("Articles", "Home");
        }

        public IActionResult DeleteComment(int id)
        {
            dataManager.Comments.DeleteCommentById(id);
            return RedirectToAction("Comments", "Home");
        }
    }
}
