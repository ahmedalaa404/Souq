using Microsoft.AspNetCore.Identity;
using Souq.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Services
{
    public interface ITokenServices
    {
        Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> UManager);




    }
}
