using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")] //Admin area üstünde çalıştığımız için belirityorum.
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategorySevice categorySevice;

        public ArticleController(IArticleService articleService, ICategorySevice categorySevice)
        { 
            this.articleService = articleService;
            this.categorySevice = categorySevice;
        }
        public async Task<IActionResult> Index()
        {
            var articles =await articleService.GetAllArticlesWithCategoryNonDeletedAsync();
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var categories = await categorySevice.GetAllCategoriesNonDeleted();
            return View(new ArticleAddViewModel { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            await articleService.CreateArticleAsync(articleAddViewModel);
            RedirectToAction("Index", "Article", new {Area="Admin"});

            var categories = await categorySevice.GetAllCategoriesNonDeleted();
            return View(new ArticleAddViewModel { Categories = categories });
        }
    }
}
