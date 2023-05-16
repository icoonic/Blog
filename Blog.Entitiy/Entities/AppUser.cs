using Microsoft.AspNetCore.Identity;
namespace Blog.Entitiy.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ImageId { get; set; } = Guid.Parse("ABC8D06F-0A4B-4724-BA66-BF978E15B82D");
        public Image Image { get; set; }    
        public ICollection<Article> Articles { get; set; }  



    }
}
