using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CategoryViewModel,Category>().ReverseMap();
            CreateMap<CategoryAddViewModel, Category>().ReverseMap();
            CreateMap<CategoryUpdateViewModel, Category>().ReverseMap();

        }
    }
}
