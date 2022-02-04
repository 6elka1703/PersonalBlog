using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Data.Entities;

namespace PersonalBlog.Data.DataAccessComponents.Interfaces
{
    public interface ICategoryes
    {
        IList<Category> GetCategoryes();
        Category GetCategoryById(int Id);
        void SaveCategory(Category category);
        void DeleteCategoryById(int Id);
    }
}
