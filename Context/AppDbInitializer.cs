using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public static class AppDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwerty123*").Result;

                if (result.Succeeded)
                {
                    
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

    }
}
