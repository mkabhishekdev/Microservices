using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenServiceAPI.Models;

namespace TokenServiceAPI.Data
{
    public class IdentityDbInit
    {
        public static async void Initialize(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            context.Database.Migrate();
            if (context.Users.Any(r => r.UserName == "me@myemail.com")) return;
            string user = "me@myemail.com";
            string password = "P@ssword1";
            await userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);
        }

    }
}
