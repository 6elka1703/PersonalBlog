using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalBlog.Areas.Admin.Models;
using PersonalBlog.Data;
using PersonalBlog.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ArticleController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
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
        public IActionResult Edit(Article article, IFormFile titleImageFile, String tagsStr)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    article.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                dataManager.Articles.SaveArticle(article);

                var tags = tagsStr.Split(",");

                List<ArticleWithTags> tagList = new List<ArticleWithTags>();
                foreach(var tag in tags)
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
