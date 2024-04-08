using FluentValidation;
using RedZone.App.Auth.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.App.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() { 
            RuleFor(X=>X.Email).NotEmpty();
            RuleFor(X => X.PhoneNumber).NotEmpty();
            RuleFor(X => X.Name).NotEmpty();
            RuleFor(X => X.password).NotEmpty();
        }
    }
}
