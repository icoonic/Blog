using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)  
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<List<ArticleViewModel>> GetAllArticlesAsync()
        {
           
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync();
            var map = mapper.Map<List<ArticleViewModel>>(articles);

            return map;
        }
    }
}
