using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IArticleService
    {
        Task<List<ArticleViewModel>> GetAllArticlesWithCategoryNonDeletedAsync();
        Task<ArticleViewModel> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task CreateArticleAsync(ArticleAddViewModel articleAddViewModel);
        Task UpdateArticleAsync(ArticleUpdateViewModel articleUpdateViewModel);
        Task SafeDeleteArticleAsync(Guid articleId);
    }
}
