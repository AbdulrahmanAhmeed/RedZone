using Microsoft.AspNetCore.Identity;
using RedZone.App.Common.Interfaces.Persistence;
using RedZone.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RedZone.Domain.Common.Errors.Errors;

namespace RedZone.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<User>? _signInManager;
        private readonly UserManager<User>? _userManager;
        public UserRepository(SignInManager<User>? signInManager, UserManager<User>? userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void AddUser(User user, string password)
        {
            var createPowerUser = _userManager?.CreateAsync(user, password).GetAwaiter().GetResult();
            var s = createPowerUser;
        }

        public User? GetUserByEmail(string email)
        {
            return _userManager?.FindByEmailAsync(email).GetAwaiter().GetResult();
        }

        public bool Login(string email, string password)
        {
            var user = _userManager?.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user != null)
            {
                var result =
                    _signInManager?.CheckPasswordSignInAsync(user, password, false).GetAwaiter().GetResult().Succeeded;
                return (bool)result;
            }
            return false;
        }
    }
}
