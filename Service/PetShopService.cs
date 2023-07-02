using Repository;
using Service.AnimelsServices;
using Service.CategoriesServices;
using Service.CommentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PetShopService : IPetShopService
    {
        public AnimalService AnimalService { get; }
        public CategoryService CategoryService { get; }
        public CommentService CommentService { get; }

        public PetShopService(IMyRepository repository)
        {
            AnimalService = new AnimalService(repository);
            CategoryService = new CategoryService(repository);
            CommentService = new CommentService(repository);
        }
    }
}
