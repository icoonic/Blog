using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Login;
using Blog.Service.Services.Abstractions;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller //class ımızın request karşılayabilme kıvamına getirmek için bunu controller dan kalıtımını alıyoruz.(türetiyoruz)
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService articleService;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            this.articleService = articleService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(Guid? categoryId, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            var articles = await articleService.GetAllByPagingAsync(categoryId, currentPage, pageSize, isAscending);
            return View(articles);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 3, bool isAscending = false)
        {
            var articles = await articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            return View(articles);
        }

        public IActionResult Privacy()
        {
            ViewResult result = View();
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Detail (Guid id)
        {
            var article = await articleService.GetArticleWithCategoryNonDeletedAsync(id);
            return View(article);
        }

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GirisYap(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginViewModel.Username);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
                    return View();
                }
            }
            else
            {

                return View();
            }
        }

    }
}