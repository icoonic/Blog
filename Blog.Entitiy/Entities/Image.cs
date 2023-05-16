using Blog.Core.Entities;

namespace Blog.Entitiy.Entities
{
    public class Image : EntityBase
    {
        public Image()
        {
        }

        public Image(string filename, string filetype, string createdBy)

        {
            FileName = filename;
            FileType = filetype;
            CreatedBy = createdBy;

        }

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> Users { get; set; }


    }
}
