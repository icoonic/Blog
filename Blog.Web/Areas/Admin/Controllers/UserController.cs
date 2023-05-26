using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.Enums;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Entitiy.ViewModels_DTOs.Images;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
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
using static Blog.Web.ResultMessages.Messages;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
       
        private readonly IUserService userService;
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toast;
        private readonly IMapper mapper;


        public UserController(IUserService userService, IValidator<AppUser> validator, IToastNotification toast, IMapper mapper)
        {
            this.userService = userService;
            this.validator = validator;
            this.toast = toast;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await userService.GetAllUserWithRoleAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRoleAsync();
            return View(new UserAddViewModel { Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddViewModel userAddViewModel)
        {
            var map = mapper.Map<AppUser>(userAddViewModel);
            var validation = await this.validator.ValidateAsync(map);
            var roles = await userService.GetAllRoleAsync();

            if (ModelState.IsValid)
            {

                var result = await userService.CreateUserAsync(userAddViewModel);
                if (result.Succeeded)
                {
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
            var user = await userService.GetAppUserByIdAsync(userId);
            var roles = await userService.GetAllRoleAsync();
            var map = mapper.Map<UserUpdateViewModel>(user);
            map.Roles = roles;

            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel userUpdateViewModel)
        {
            var user = await userService.GetAppUserByIdAsync(userUpdateViewModel.Id);

            if (user != null)
            {
                var roles = await userService.GetAllRoleAsync();

                if (ModelState.IsValid)
                {
                    var map = mapper.Map(userUpdateViewModel, user);
                    var validation = await this.validator.ValidateAsync(map);
                    if (validation.IsValid)
                    {
                        user.UserName = userUpdateViewModel.Email;
                        user.SecurityStamp = Guid.NewGuid().ToString();
                        var result = await userService.UpdateUserAsync(userUpdateViewModel);
                        if (result.Succeeded)
                        {
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
            var result = await userService.DeleteUserAsync(userId);
           
            if (result.identityResult.Succeeded)
            {
                toast.AddSuccessToastMessage(Messages.User.Delete(result.email), new ToastrOptions { Title = "İşlem Başarılı" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }
            else
            {
                result.identityResult.AddToIdentityModelState(this.ModelState);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await userService.GetUserProfileAsync();
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel userProfileViewModel)
        {

            if (ModelState.IsValid)
            {
               var result = await userService.UserProfileUpdateAsync(userProfileViewModel);
                if (result)
                {
                    toast.AddSuccessToastMessage("Profil Güncelleme İşlemi Tamamlandı.", new ToastrOptions { Title = "İşlem Başarılı" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });

                }
                else
                {
                    var profile = await userService.GetUserProfileAsync();
                    toast.AddErrorToastMessage("Profil Güncelleme İşlemi Tamamlanamadı.", new ToastrOptions { Title = "İşlem Başarısız" });
                    return View(profile);
                }
            }
            else 
               return NotFound();
        }
    }
}

