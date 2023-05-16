using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Blog.Web.ResultMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IToastNotification toast;
        private readonly IMapper mapper;
        

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,IToastNotification toast, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.toast = toast;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserViewModel>>(users);

            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                item.Role = role;
            }

            return View(map);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(new UserAddViewModel { Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            var map = mapper.Map<AppUser>(userAddViewModel);
            var roles = await roleManager.Roles.ToListAsync();

            if (ModelState.IsValid)
            {
                map.UserName = userAddViewModel.Email;
                var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddViewModel.Password) ? " " : userAddViewModel.Password);

                if (result.Succeeded)
                {
                    var findRole = await roleManager.FindByIdAsync(userAddViewModel.RoleId.ToString());
                    await userManager.AddToRoleAsync(map, findRole.ToString());
                    toast.AddSuccessToastMessage(Messages.User.Add(userAddViewModel.Email), new ToastrOptions { Title = "İşlem Başarılı" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
                else
                {
                    foreach (var errors in result.Errors)
                        ModelState.AddModelError("", errors.Description);
                    return View(new UserAddViewModel { Roles = roles });
                }
            }

            return View(new UserAddViewModel { Roles = roles });
        }
    }
}
