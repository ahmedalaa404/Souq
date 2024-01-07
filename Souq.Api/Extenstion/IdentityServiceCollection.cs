using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Souq.Core.Entites.Identity;
using Souq.Repositorey.DataBase.Identity;
using System.Text;

namespace Souq.Api.Extenstion
{
    public static class IdentityServiceCollection
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection Services,IConfiguration Config)
        {
            Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;    
            }).AddJwtBearer(O =>
            {
                O.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Config["Jwt:Isuuer"],
                    ValidateAudience = true,

                    ValidAudience = Config["Jwt:audience"],
                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"])),
                    ValidateIssuerSigningKey = true,




                };
            });
            return Services;
        }

    }
}
