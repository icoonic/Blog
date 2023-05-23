using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.Enums;
using Blog.Entitiy.ViewModels_DTOs.Articles;
using Blog.Entitiy.ViewModels_DTOs.Images;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IImageHelper imageHelper;
        private readonly IValidator<AppUser> validator;
        private readonly IToastNotification toast;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;


        public UserController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager, IImageHelper imageHelper, IValidator<AppUser> validator, IToastNotification toast, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
            this.imageHelper = imageHelper;
            this.validator = validator;
            this.toast = toast;
            this.signInManager = signInManager;
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
                result.AddToIdentityModelState(this.ModelState);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var map =mapper.Map<UserProfileViewModel>(user);
            return View(map);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel userProfileViewModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var isVerified = await userManager.CheckPasswordAsync(user, userProfileViewModel.CurrentPassword);
                if (isVerified && userProfileViewModel.NewPassword != null && userProfileViewModel.Photo != null)
                {
                    var result = await userManager.ChangePasswordAsync(user, userProfileViewModel.CurrentPassword, userProfileViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        await userManager.UpdateSecurityStampAsync(user);
                        await signInManager.SignOutAsync();
                        await signInManager.PasswordSignInAsync(user, userProfileViewModel.NewPassword, true, false);

                        user.FirstName = userProfileViewModel.FirstName;
                        user.LastName = userProfileViewModel.LastName;
                        user.PhoneNumber = userProfileViewModel.PhoneNumber;

                        var imageUpload = await imageHelper.Upload($"{userProfileViewModel.FirstName}{userProfileViewModel.LastName}", userProfileViewModel.Photo, ImageType.User);
                        Image image = new(imageUpload.FullName, userProfileViewModel.Photo.ContentType, user.Email);

                        await unitOfWork.GetRepository<Image>().AddAsync(image);

                        user.ImageId = image.Id;

                        await userManager.UpdateAsync(user);
                        await unitOfWork.SaveAsync();
                        toast.AddSuccessToastMessage("Şifre başarıyla değiştirildi.");
                        return View();
                    }
                    else

                        result.AddToIdentityModelState(ModelState); return View();

                }
                else if (isVerified && userProfileViewModel.Photo != null)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    user.FirstName = userProfileViewModel.FirstName;
                    user.LastName = userProfileViewModel.LastName;
                    user.PhoneNumber = userProfileViewModel.PhoneNumber;

                    var imageUpload = await imageHelper.Upload($"{userProfileViewModel.FirstName}{userProfileViewModel.LastName}", userProfileViewModel.Photo, ImageType.User);
                    Image image = new(imageUpload.FullName, userProfileViewModel.Photo.ContentType, user.Email);

                    await unitOfWork.GetRepository<Image>().AddAsync(image);

                    user.ImageId = image.Id;

                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();
                 
                    toast.AddSuccessToastMessage("Bigiler başarıyla değiştirildi.");
                    return View();
                }
                else
                    toast.AddErrorToastMessage("Hata"); return View();

            }

            return View();
        }
    }
}

