using Blog.Entitiy.ViewModels_DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface ICategorySevice
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesNonDeleted();
    }
}
