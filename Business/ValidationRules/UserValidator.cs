using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth cannot be empty");
            RuleFor(x => x.DateOfBirth).Must(BeAValidDate).WithMessage("Date of birth is not valid");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date <= DateTime.Today && date > DateTime.Today.AddYears(-100);
        }

    }
}
