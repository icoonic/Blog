using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.AutoMapper.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile() 
        {
            CreateMap<ArticleViewModel, Article>().ReverseMap();
            CreateMap<ArticleUpdateViewModel, Article>().ReverseMap();
            CreateMap<ArticleUpdateViewModel, ArticleViewModel>().ReverseMap();
            CreateMap<ArticleAddViewModel, Article>().ReverseMap();
        }

    }
}
