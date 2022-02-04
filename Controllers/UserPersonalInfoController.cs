using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PersonalBlog.Models;
using PersonalBlog.Data;
using PersonalBlog.Data.Entities;
using PersonalBlog.Services;

namespace PersonalBlog.Controllers
{
    [Authorize]
    public class UserPersonalInfoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataManager _dataManager;

        public UserPersonalInfoController(UserManager<ApplicationUser> userManager, DataManager dataManager)
        {
            this._userManager = userManager;
            this._dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id = "")
        {
            ApplicationUser user;
            if (id == string.Empty)
            {
                user = await _userManager.GetUserAsync(User);
                ViewData["Title"] = "Изменение личных данных";
            }
            else
            {
                user = await _userManager.FindByIdAsync(id);
                ViewData["Title"] = "Редактирование личных данных пользователя";
            };
               
            if (user == null)
            {
                return NotFound();
            }

            return View(await GetEditUserPersonalInfoViewModel(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserPersonalInfoViewModel model, IFormFile userPhotoFile, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;

                    if (userPhotoFile != null)
                    {
                        StaticFilesService staticFilesService = new StaticFilesService(hostingEnvironment);
                        user.PhotoPath = staticFilesService.SaveFile(userPhotoFile, "images/");
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        if (User.IsInRole("admin"))
                            return RedirectToAction("Index", "Users", new { area = "Admin" });
                        else
                            return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<EditUserPersonalInfoViewModel> GetEditUserPersonalInfoViewModel(ApplicationUser user)
        {
            return new EditUserPersonalInfoViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhotoPath = user.GetPhotoPath()
            };
        }
    }
}
