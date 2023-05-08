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
            var imageId = Guid.Parse("ABC8D06F-0A4B-4724-BA66-BF978E15B82D");
            var article = new Article(articleAddViewModel.Title, articleAddViewModel.Content, userId, articleAddViewModel.CategoryId, imageId);
            

            await unitOfWork.GetRepository<Article>().AddAsync(article);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<ArticleViewModel>> GetAllArticlesWithCategoryNonDeletedAsync()
        {
           
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x=>!x.IsDeleted, x=>x.Category);
            var map = mapper.Map<List<ArticleViewModel>>(articles);

            return map;
        }
        public async Task<ArticleViewModel> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
        {

            var articles = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id ==articleId, x => x.Category);
            var map = mapper.Map<ArticleViewModel>(articles);

            return map;
        }
        public async Task UpdateArticleAsync(ArticleUpdateViewModel articleUpdateViewModel) 
        {
            var article = await unitOfWork.GetRepository<Article>().GetAsync(x => !x.IsDeleted && x.Id == articleUpdateViewModel.Id, x => x.Category);

            article.Title = articleUpdateViewModel.Title;
            article.Content = articleUpdateViewModel.Content;
            article.CategoryId = articleUpdateViewModel.CategoryId; 

            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();

        }
        public async Task SafeDeleteArticleAsync(Guid articleId)
        {
            var article = await unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);
            article.IsDeleted = true;
            article.DeletedDate = DateTime.Now;
            await unitOfWork.GetRepository<Article>().UpdateAsync(article);
            await unitOfWork.SaveAsync();
        }

    }
}
