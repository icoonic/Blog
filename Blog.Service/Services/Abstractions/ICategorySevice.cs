using Blog.Entitiy.Entities;
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
        Task<List<CategoryViewModel>> GetAllCategoriesNonDeleted();
        Task<List<CategoryViewModel>> GetAllCategoriesDeleted();
        Task CreateCategoryAsync(CategoryAddViewModel categoryAddViewModel);
        Task<Category> GetCategoryByGuid(Guid id);
        Task<string> UpdateCategoryAsync(CategoryUpdateViewModel categoryUpdateViewModel);
        Task<string> SafeDeleteCategoryAsync(Guid categoryId);
        Task<string> UndoDeleteCategoryAsync(Guid categoryId);
    }
}
