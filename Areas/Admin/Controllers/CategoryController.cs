using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoryController : Controller
    {
        private readonly DataManager dataManager;

        public CategoryController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = id == default ? new Category() : dataManager.Categoryes.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                dataManager.Categoryes.SaveCategory(category);
                return RedirectToAction("Categoryes", "Home");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = dataManager.Categoryes.GetCategoryById(id);
            if(category.Articles.Count > 0)
            {
                return Content($"Невозможно удалить категорию - {category.Name}, т.к. есть статьи в этой категории.");
            }

            dataManager.Categoryes.DeleteCategoryById(id);
            return RedirectToAction("Categoryes", "Home");
        }

    }
}
