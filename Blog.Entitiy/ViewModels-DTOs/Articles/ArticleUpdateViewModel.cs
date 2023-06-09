﻿using Blog.Entitiy.Entities;
using Blog.Entitiy.ViewModels_DTOs.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entitiy.ViewModels_DTOs.Articles
{
    public class ArticleUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public Image Image { get; set; }
        public IFormFile? Photo { get; set; } // güncelleme işlemi yaparken fotoğraf eklemeyebilirim o yüzden 
        public IList<CategoryViewModel> Categories { get; set; }
    }
}
