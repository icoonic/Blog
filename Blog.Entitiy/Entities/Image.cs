using Blog.Core.Entities;
using Blog.Entitiy.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entitiy.Entities
{
    public class Image : EntityBase
    {
        public Image()
        {
        }
        public Image(string filename, string filetype,)
        {
            FileName = filename;
            FileType = filetype;
        }

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> Users { get; set; }


    }
}
