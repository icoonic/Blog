using AutoMapper;
using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Images;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Blog.Service.Extensions;
using Blog.Web.ResultMessages;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toast;
        private readonly IMapper mapper;


        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IValidator<AppUser> validator, IToastNotification toast, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.validator = validator;
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
            var validation = await this.validator.ValidateAsync(map);
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

                    result.AddToIdentityModelState(this.ModelState);
                    validation.AddToModelState(this.ModelState);
                    return View(new UserAddViewModel { Roles = roles });
                }
            }

            return View(new UserAddViewModel { Roles = roles });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var roles = await roleManager.Roles.ToListAsync();
            var map = mapper.Map<UserUpdateViewModel>(user);
            map.Roles = roles;

            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel userUpdateViewModel)
        {
            var user = await userManager.FindByIdAsync(userUpdateViewModel.Id.ToString());

            if (user != null)
            {
                var userRole = string.Join("", await userManager.GetRolesAsync(user));
                var roles = await roleManager.Roles.ToListAsync();

                if (ModelState.IsValid)
                {
                    var map = mapper.Map(userUpdateViewModel, user);
                    var validation = await this.validator.ValidateAsync(map);
                    if (validation.IsValid)
                    {
                        user.UserName = userUpdateViewModel.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            await userManager.RemoveFromRoleAsync(user, userRole);
                            var findRole = await roleManager.FindByIdAsync(userUpdateViewModel.RoleId.ToString());
                            await userManager.AddToRoleAsync(user, findRole.Name);
                            toast.AddSuccessToastMessage(Messages.User.Update(userUpdateViewModel.Email), new ToastrOptions { Title = "İşlem Başarılı" });
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }

                        else
                        {
                            result.AddToIdentityModelState(this.ModelState);

                            return View(new UserUpdateViewModel { Roles = roles });
                        }
                    }
                    else
                    {
                        validation.AddToModelState(this.ModelState);
                        return View(new UserUpdateViewModel { Roles = roles });
                    }

                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            var result = await userManager.DeleteAsync(user);
           
            if (result.Succeeded)
            {
                toast.AddSuccessToastMessage(Messages.User.Delete(user.Email), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });

            }
            else
            {
                foreach (var errors in result.Errors)
                    ModelState.AddModelError("", errors.Description);
            }
            return NotFound();


        }
    }
}
