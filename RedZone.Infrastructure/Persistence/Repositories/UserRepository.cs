using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedZone.App.Common.Interfaces.Auth;
using RedZone.App.Common.Interfaces.Persistence;
using RedZone.App.Services.Auth.Common;
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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RedZoneDB _redZoneDB;
        public UserRepository(SignInManager<User>? signInManager, UserManager<User>? userManager,
            IJwtTokenGenerator jwtTokenGenerator, RedZoneDB redZoneDB)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _signInManager = signInManager;
            _userManager = userManager;
            _redZoneDB = redZoneDB;
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

        public AuthResult Login(string email, string password)
        {
            var user = _userManager?.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user != null)
            {
                var result =
                    _signInManager?.CheckPasswordSignInAsync(user, password, false).GetAwaiter().GetResult().Succeeded;
                if ((bool)result)
                {

                    var token = _jwtTokenGenerator.GenerateToken(user);
                    var refreshToken = _jwtTokenGenerator.RefreshTokenGeneration();
                    user.RefreshToken = refreshToken;
                    //TODO: case: RefreshTokenExpireDate
                    //user.CreatedDate = DateTime.UtcNow.AddDays(7); 
                    _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                    return new AuthResult("Auth", refreshToken, false, token);
                }
            }
            return null;
        }

        public AuthResult RefreshToken(string token, string refreshToken)
        {
            try
            {
                var user = _redZoneDB.Users.Where(
                    U => U.RefreshToken == refreshToken
                    ).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                else
                {
                    var output = _jwtTokenGenerator.GetInfoJwtToken(token);
                    var newToken = _jwtTokenGenerator.GenerateAccessToken(output.Claims);
                    var newRefreshToken = _jwtTokenGenerator.RefreshTokenGeneration();
                    user.RefreshToken = newRefreshToken;
                    _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                    return new AuthResult("Auth", newRefreshToken, false, newToken);
                }
            }
            catch (Exception ex) {
                return null;
            }



        }

        public bool VerifyEmail(string userId, string code)
        {
            var user = _userManager?.FindByIdAsync(userId).GetAwaiter().GetResult();
            if (user == null) return false;
            var result = _userManager?.ConfirmEmailAsync(user, code).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
