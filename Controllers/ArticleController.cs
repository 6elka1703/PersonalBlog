using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Data.Entities;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly DataManager dataManager;

        public ArticleController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public IActionResult Index(int id)
        {
            var article = dataManager.Articles.GetArticleById(id);
            if(article == null)
            {
                return NotFound();
            }

            var tags = dataManager.ArticleWithTags.GetTagsByArticleId(id);
            var comments = dataManager.Comments.GetCommentsByArticleId(id);

            ArticleViewModel viewModel = new ArticleViewModel
            {
                Article = article,
                Tags = tags,
                Comments = comments
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Comment(Comment comment)
        {
            dataManager.Comments.SaveComment(comment);
            return RedirectToAction("Index", new { id = comment.ArticleId} );
        }
    }
}
