using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
namespace Blog.Entitiy.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntitiyBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ImageId { get; set; } = Guid.Parse("f3e0e426-832c-4b91-1a65-08db5cf963f2");
        public Image Image { get; set; }    
        public ICollection<Article> Articles { get; set; }  



    }
}
