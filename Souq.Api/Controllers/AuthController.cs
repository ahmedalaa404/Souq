using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS.IdentityDto;
using Souq.Api.Errors;
using Souq.Core.Entites.Identity;

namespace Souq.Api.Controllers
{
    public class AuthController:ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> SignIn)
        {
            _userManager = userManager;
            _signIn = SignIn;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLogin Model)
        {

                var User=await _userManager.FindByEmailAsync(Model.Email);
                if(User != null)
                {

                var Sign = await _signIn.CheckPasswordSignInAsync(User, Model.Password, false);
                if (Sign.Succeeded)
                {
                    var Resulte = new UserDto()
                    {
                        DisplayName = User.DisplayName,
                        Token="Hambozo Is Sign In",
                        Email= User.Email
                    };
                    return Ok(Resulte);
                }
                return Unauthorized(new ApiResponse(401));


            }
            return Unauthorized(new ApiResponse(401));
            

        }





    }
}
