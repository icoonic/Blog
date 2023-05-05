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


        public async Task CreateArticleAsync(ArticleAddViewModel articleAddViewModel)
        {
            var userId = Guid.Parse("36E8F3D6-0E25-4D65-8CA6-81CA01C49461");

            var article = new Article
            {
                Title = articleAddViewModel.Title,
                Content = articleAddViewModel.Content,
                CategoryId = articleAddViewModel.CategoryId,
                UserId = userId,
            };
            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleViewModel>> GetAllArticlesWithCategoryNonDeletedAsync()
        {
           
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted, x=>x.Category);
            var map = mapper.Map<List<ArticleViewModel>>(articles);

            return map;
        }
    }
}
