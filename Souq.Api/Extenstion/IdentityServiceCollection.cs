using Microsoft.AspNetCore.Identity;
using Souq.Core.Entites.Identity;
using Souq.Repositorey.DataBase.Identity;

namespace Souq.Api.Extenstion
{
    public static class IdentityServiceCollection
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication();
            return Services;
        }

    }
}
