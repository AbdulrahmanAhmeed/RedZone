
using Microsoft.AspNetCore.Identity;
using RedZone.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace RedZone.Domain.Users
{
    public sealed class User : IdentityUser
    {
        public string Name { get; private set; }

        // for the mobile (or web) JWT users
        public string? RefreshToken { get; set; }

        

        private User(
        string name,
        string email,
        string phoneNumber,
        string userName = "User"
        )
        
        {
            UserName = userName;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        

        public static User Create(
        string name,
        string phoneNumber,
        string email,
        string userName = "User")
        {
            return new User(
                name,
                email,
                phoneNumber,
                userName);
        }
    }

    

}
