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
        Task<List<ArticleViewModel>> GetAllArticlesWithCategoryDeletedAsync();
        Task<ArticleViewModel> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
        Task CreateArticleAsync(ArticleAddViewModel articleAddViewModel);
        Task<string> UpdateArticleAsync(ArticleUpdateViewModel articleUpdateViewModel);
        Task<string> SafeDeleteArticleAsync(Guid articleId);
        Task<string> UndoDeleteArticleAsync(Guid articleId);
        Task<ArticleListViewModel> GetAllByPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        Task<ArticleListViewModel> SearchAsync(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false);
        



    }
}
