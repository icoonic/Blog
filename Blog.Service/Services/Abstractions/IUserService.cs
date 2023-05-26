using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUserWithRoleAsync();
        Task<List<AppRole>> GetAllRoleAsync();
        Task<IdentityResult> CreateUserAsync(UserAddViewModel userAddViewModel);
        Task<IdentityResult> UpdateUserAsync(UserUpdateViewModel userUpdateViewModel);
        Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId);


        Task<AppUser> GetAppUserByIdAsync(Guid userId);
        Task<string> GetUserRoleAsync(AppUser user);
        Task<UserProfileViewModel> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileViewModel userProfileViewModel);



    }
}
