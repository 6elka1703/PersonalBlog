using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data.DataAccessComponents.Interfaces;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.EntityFrameworkModels
{
    public class Categoryes : ICategoryes
    {
        private readonly ApplicationDbContext context;

        public Categoryes(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> GetCategoryes()
        {
            return context.Categories.Include(x => x.Articles);
        }

        public Category GetCategoryById(int Id)
        {
            return context.Categories
                .Include(x => x.Articles)
                .FirstOrDefault(p => p.Id == Id);
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == default(int))
            {
                context.Entry(category).State = EntityState.Added;
            }
            else
            {
                context.Entry(category).State = EntityState.Modified;
            }

            context.SaveChanges();
        }

        public void DeleteCategoryById(int Id)
        {
            if (Id != null)
            {
                Category category = GetCategoryById(Id);
                if (category != null)
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();
                }
            }
        }
    }
}
