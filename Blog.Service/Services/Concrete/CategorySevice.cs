using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blog.Service.Services.Concrete
{
    public class CategorySevice : ICategorySevice
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public CategorySevice(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;

        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesNonDeleted()
        {

            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
            var map = mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }
        public async Task CreateCategoryAsync(CategoryAddViewModel categoryAddViewModel)
        {

            var userEmail = _user.GetLoggedInEmail();
            Category category = new(categoryAddViewModel.Name, userEmail);
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();

        }
        public async Task<Category> GetCategoryByGuid(Guid id)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            return category;
        }
        public async Task<string> UpdateCategoryAsync(CategoryUpdateViewModel categoryUpdateViewModel)
        {

            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<Category>().GetAsync(x => !x.IsDeleted && x.Id == categoryUpdateViewModel.Id);

            category.Name = categoryUpdateViewModel.Name;
            category.ModifiedBy = userEmail;
            category.ModifiedDate = DateTime.Now;

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
            return category.Name;

        }

        public async Task<string> SafeDeleteCategoryAsync(Guid categoryId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);
            category.IsDeleted = true;
            category.DeletedDate = DateTime.Now;
            category.DeletedBy = userEmail;

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
            return category.Name;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesDeleted()
        {
            var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsDeleted);
            var map = mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }

        public async Task<string> UndoDeleteCategoryAsync(Guid categoryId)
        {
            var userEmail = _user.GetLoggedInEmail();
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryId);
            category.IsDeleted = false;
            category.DeletedDate = null;
            category.DeletedBy = null;

            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
            return category.Name;
        }
    }

}

