using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")] //Admin area üstünde çalıştığımız için belirityorum.
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategorySevice categorySevice;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;

        public ArticleController(IArticleService articleService, ICategorySevice categorySevice,IMapper mapper, IValidator<Article> validator)
        { 
            this.articleService = articleService;
            this.categorySevice = categorySevice;
            this.mapper = mapper;
            this.validator = validator;
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
            var map = mapper.Map<Article>(articleAddViewModel);
            var result = await validator.ValidateAsync(map);

            if(result.IsValid)
            {
                await articleService.CreateArticleAsync(articleAddViewModel);
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
            }
            else 
            {
                result.AddToModelState(this.ModelState);
                var categories = await categorySevice.GetAllCategoriesNonDeleted();
                return View(new ArticleAddViewModel { Categories = categories });
            }

        
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid articleId) //makaleyi güncellemek istediğimizde o makalenin id si gelmeli o makaleye göre işlem yapmak için
        {
            var articles = await articleService.GetArticleWithCategoryNonDeletedAsync(articleId);
            var categories = await categorySevice.GetAllCategoriesNonDeleted();
            
            var articleUpdateViewModel = mapper.Map<ArticleUpdateViewModel>(articles);
            articleUpdateViewModel.Categories = categories;

            return View(articleUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            var map = mapper.Map<Article>(articleUpdateViewModel);
            var result = await validator.ValidateAsync(map);
            if(result.IsValid)
            {
                await articleService.UpdateArticleAsync(articleUpdateViewModel);
            }
            else
            {
                result.AddToModelState(this.ModelState);
            }

            

            var categories = await categorySevice.GetAllCategoriesNonDeleted();

            articleUpdateViewModel.Categories = categories;

            return View(articleUpdateViewModel);
        }
        public async Task<IActionResult> Delete(Guid articleId)
        {
            await articleService.SafeDeleteArticleAsync(articleId);
            return RedirectToAction("Index", "Article", new {Area ="Admin"});
        }
    }
}
