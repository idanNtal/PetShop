using Service.AccountServices;
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
    public interface IPetShopService
    {
        AnimalService AnimalService { get; }
        CategoryService CategoryService { get; }
        CommentService CommentService { get; }
    }
}
