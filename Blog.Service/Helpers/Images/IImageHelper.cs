using Blog.Entitiy.Enums;
using Blog.Entitiy.ViewModels_DTOs.Images;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Helpers.Images
{
    public interface IImageHelper
    {
        Task<ImageUploadedViewModel> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null );
        void Delete ( string imageName );
    }
}
