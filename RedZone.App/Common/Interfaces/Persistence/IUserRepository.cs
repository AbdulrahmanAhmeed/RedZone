using RedZone.App.Services.Auth.Common;
using RedZone.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user,string password);
        AuthResult Login(string email,string password);
        AuthResult RefreshToken(string token, string refreshToken);
        bool VerifyEmail(string userId,string code);
    }
}
