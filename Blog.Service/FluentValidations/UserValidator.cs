using Blog.Entitiy.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.FluentValidations
{
    public class UserValidator : AbstractValidator<AppUser>
    {
        public UserValidator() 
        {
            RuleFor(x=>x.FirstName)
                .NotEmpty()
                .MaximumLength(40)
                .MinimumLength(2)
                .WithName("İsim");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .MaximumLength(40)
               .MinimumLength(2)
               .WithName("Soyisim");

            RuleFor(x => x.PhoneNumber)
               .NotEmpty()
               .MaximumLength(11)
               .WithName("Telefon Numarası");

        }

    }
}
