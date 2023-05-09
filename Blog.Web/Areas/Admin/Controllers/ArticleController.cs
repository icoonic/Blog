using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")] //Adminde üstünde çalıştığımız için belirityorum.
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICategorySevice categorySevice;
        private readonly IMapper mapper;
        private readonly IValidator<Article> validator;
        private readonly IToastNotification toast;

        public ArticleController(IArticleService articleService, ICategorySevice categorySevice,IMapper mapper, IValidator<Article> validator, IToastNotification toast)
        { 
            this.articleService = articleService;
            this.categorySevice = categorySevice;
            this.mapper = mapper;
            this.validator = validator;
            this.toast = toast;
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
                toast.AddSuccessToastMessage(Messages.Article.Add(articleAddViewModel.Title), new ToastrOptions { Title = "" });
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
               var title = await articleService.UpdateArticleAsync(articleUpdateViewModel);
                toast.AddSuccessToastMessage(Messages.Article.Update(title), new ToastrOptions() { Title = "" });
                return RedirectToAction("Index", "Article", new { Area = "Admin" });
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
            var title = await articleService.SafeDeleteArticleAsync(articleId);
            toast.AddSuccessToastMessage(Messages.Article.Delete(title), new ToastrOptions() { Title = "" });
            return RedirectToAction("Index", "Article", new {Area ="Admin"});
        }
    }
}
