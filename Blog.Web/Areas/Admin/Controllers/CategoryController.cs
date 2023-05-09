using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.ResultMessages;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategorySevice categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public CategoryController(ICategorySevice categoryService, IValidator<Category> validator, IMapper mapper, IToastNotification toastNotification)
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var categoies = await categoryService.GetAllCategoriesNonDeleted();
            return View(categoies);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddViewModel categoryAddViewModel)
        {
            var map = mapper.Map<Category>(categoryAddViewModel);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddViewModel);
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

                result.AddToModelState(this.ModelState);
                return View();



        }
    }
}


