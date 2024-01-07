using Microsoft.AspNetCore.Identity;
using Souq.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Repositorey.DataBase.Identity
{
    public static class AppIdentityDbcontextSeeding
    {
        public static async Task  SeedAccount(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any()) 
            {
                    var User=new AppUser()
                    { 
                        DisplayName="Ahmed Nasser",
                        Email="AhmedalaaYassin6@gmail.com",
                        UserName= "AhmedalaaYassin6",
                        PhoneNumber="01005327706"
                    };
                var Users= await userManager.CreateAsync(User,"Ahmed01202898890");
            }



        }


    }
}
