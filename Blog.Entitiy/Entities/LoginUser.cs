using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entitiy.Entities
{
    public class 
		
		LoginUser : IdentityUser<Guid>, IEntitiyBase
	{
		
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
        public bool RememberMe { get; set; }

    }


}
