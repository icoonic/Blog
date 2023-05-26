using AutoMapper;
using Blog.Data.UnitOfWorks;
using Blog.Entitiy.Entities;
using Blog.Entitiy.Enums;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Blog.Service.Extensions;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageHelper imageHelper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork, IImageHelper imageHelper, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.unitOfWork = unitOfWork;
            this.imageHelper = imageHelper;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddViewModel userAddViewModel)
        {
            var map = mapper.Map<AppUser>(userAddViewModel);
            map.UserName = userAddViewModel.Email;
            var result = await userManager.CreateAsync(map, string.IsNullOrEmpty(userAddViewModel.Password) ? " " : userAddViewModel.Password);
            if (result.Succeeded)
            {
                var findRole = await roleManager.FindByIdAsync(userAddViewModel.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
                return result;
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
               return (result,user.Email);
            else
                return (result, null);
            
        }

        public async Task<List<AppRole>> GetAllRoleAsync()
        {
           return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserViewModel>> GetAllUserWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserViewModel>>(users);

            foreach (var item in map)
            {
                var findUser = await userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                item.Role = role;
            }
            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateViewModel userUpdateViewModel)
        {
            var user = await GetAppUserByIdAsync(userUpdateViewModel.Id);
            var userRole = await GetUserRoleAsync(user);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await roleManager.FindByIdAsync(userUpdateViewModel.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
                return result;
        }
        public async Task<UserProfileViewModel> GetUserProfileAsync()
        {
            var userId = _user.GetLoggedInUserId();
            var getUserWithImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId, x => x.Image);
            var map = mapper.Map<UserProfileViewModel>(getUserWithImage);
            map.Image.FileName = getUserWithImage.Image.FileName;

            return map;
        }
        private async Task<Guid> UploadImageForUser(UserProfileViewModel userProfileViewModel)
        {
            var userEmail = _user.GetLoggedInEmail();
            var imageUpload = await imageHelper.Upload($"{userProfileViewModel.FirstName}{userProfileViewModel.LastName}", userProfileViewModel.Photo, ImageType.User);
            Image image = new(imageUpload.FullName, userProfileViewModel.Photo.ContentType, userEmail);

            await unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;

        }
        public async Task<bool> UserProfileUpdateAsync(UserProfileViewModel userProfileViewModel)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userId);

            var isVerified = await userManager.CheckPasswordAsync(user, userProfileViewModel.CurrentPassword);
            if (isVerified && userProfileViewModel.NewPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(user, userProfileViewModel.CurrentPassword, userProfileViewModel.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(user, userProfileViewModel.NewPassword, true, false);

                    mapper.Map(userProfileViewModel, user); 

                    if(userProfileViewModel.Photo != null)
                       user.ImageId = await UploadImageForUser(userProfileViewModel);

                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();

                    return true;
                }
                else
                    return false;
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);
                mapper.Map(userProfileViewModel, user);

                if (userProfileViewModel.Photo != null)
                    user.ImageId = await UploadImageForUser(userProfileViewModel);

                await userManager.UpdateAsync(user);
                await unitOfWork.SaveAsync();

                return true;
            }
            
            else
                return false;

        }      

        
    }
}
