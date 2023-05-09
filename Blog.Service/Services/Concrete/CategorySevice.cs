using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
    }
}
