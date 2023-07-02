using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace Service.CategoriesServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Category>> GetAllCategoriesWithAminalsAsync();
        Task<Category> GetCategoryByIdAsync(int CategoryId);
    }
}
