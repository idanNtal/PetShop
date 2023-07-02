using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace Service.CategoriesServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMyRepository _repo;

        public CategoryService(IMyRepository repository) => _repo = repository;


        private IEnumerable<Category> GetAllCategories() => _repo.Categories.ToList();
        public Task<IEnumerable<Category>> GetAllCategoriesAsync() => Task.Run(() => GetAllCategories());


        private IEnumerable<Category> GetAllCategoriesWithAminals() => _repo.GetCategoriesWithAminals().ToList();
        public Task<IEnumerable<Category>> GetAllCategoriesWithAminalsAsync() => Task.Run(() => GetAllCategoriesWithAminals());


        private Category GetCategoryById(int CategoryId)
        {
            var res = _repo.Categories.First(category => category.Id == CategoryId);
            return res;
        }
        public Task<Category> GetCategoryByIdAsync(int CategoryId) => Task.Run(() => GetCategoryById(CategoryId));
    }
}
