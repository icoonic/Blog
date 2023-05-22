using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategorySevice categoryService;
        private readonly IValidator<Category> validator;
        private readonly IMapper mapper;
        private readonly IToastNotification toast;

        public CategoryController(ICategorySevice categoryService, IValidator<Category> validator, IMapper mapper, IToastNotification toastNotification)
        {
            this.categoryService = categoryService;
            this.validator = validator;
            this.mapper = mapper;
            this.toast = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesNonDeleted();
            return View(categories);
        }
        public async Task<IActionResult> DeletedCategory()
        {
            var categories = await categoryService.GetAllCategoriesDeleted();
            return View(categories);
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
                toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddViewModel.Name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });
            }

             result.AddToModelState(this.ModelState);
             return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddWithAjax([FromBody] CategoryAddViewModel categoryAddViewModel)
        {
            var map = mapper.Map<Category>(categoryAddViewModel);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                await categoryService.CreateCategoryAsync(categoryAddViewModel);
                toast.AddSuccessToastMessage(Messages.Category.Add(categoryAddViewModel.Name), new ToastrOptions { Title = "İşlem Başarılı" });

                return Json(Messages.Category.Add(categoryAddViewModel.Name));
            }
            else
            {
                toast.AddErrorToastMessage(result.Errors.First().ErrorMessage, new ToastrOptions { Title = "İşlem Başarısız" });
                return Json(result.Errors.First().ErrorMessage);
            }
        }





        [HttpGet]
        public async Task<IActionResult> Update(Guid categoryId)
        {
           var category = await categoryService.GetCategoryByGuid(categoryId);
           var map = mapper.Map<Category, CategoryUpdateViewModel>(category);

           return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel categoryUpdateViewModel)
        {
            var map = mapper.Map<Category>(categoryUpdateViewModel);
            var result = await validator.ValidateAsync(map);

            if (result.IsValid)
            {
                var name = await categoryService.UpdateCategoryAsync(categoryUpdateViewModel);
                toast.AddSuccessToastMessage(Messages.Category.Update(name), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "Category", new { Area = "Admin" });

            }
            result.AddToModelState(this.ModelState);
            return View();
        }
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            var name = await categoryService.SafeDeleteCategoryAsync(categoryId);
            toast.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions() { Title = "" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }
        public async Task<IActionResult> UndoDelete(Guid categoryId)
        {
            var name = await categoryService.UndoDeleteCategoryAsync(categoryId);
            toast.AddSuccessToastMessage(Messages.Category.Delete(name), new ToastrOptions() { Title = "" });
            return RedirectToAction("Index", "Category", new { Area = "Admin" });
        }


    }
}
