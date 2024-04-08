
using Microsoft.AspNetCore.Identity;
using RedZone.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace RedZone.Domain.Users
{
    public sealed class User : IdentityUser
    {
        public string Name { get; private set; }

        // for the mobile (or web) JWT users
        public string? RefreshToken { get; private set; }

        

        private User(
        string name,
        string email,
        string phoneNumber
        )
        
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static User Create(
        string name,
        string phoneNumber,
        string email)
        {
            return new User(
                name,
                email,
                phoneNumber);
        }
    }

    

}
