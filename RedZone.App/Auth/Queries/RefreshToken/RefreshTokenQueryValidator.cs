using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Auth.Queries.RefreshToken
{
    public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenQueryValidator() 
        {
            RuleFor(X => X.RefreshToken).NotEmpty();
            RuleFor(X => X.Token).NotEmpty();
        }
    }
}
