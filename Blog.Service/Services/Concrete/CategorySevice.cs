using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class CategorySevice : ICategorySevice
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategorySevice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesNonDeleted()
        {
          var categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
          var map = mapper.Map<List<CategoryViewModel>>(categories);
            return map;
        }
    }
}
