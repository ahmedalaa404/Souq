using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Souq.Api.DTOS.IdentityDto;
using Souq.Api.Errors;
using Souq.Core.Entites.Identity;
using Souq.Core.Repositories;
using System.Security.Claims;

namespace Souq.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly ITokenServices tokenServices;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> SignIn ,ITokenServices TokenServices)
        {
            _userManager = userManager;
            _signIn = SignIn;
            tokenServices = TokenServices;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLogin Model)
        {

            var User = await _userManager.FindByEmailAsync(Model.Email);
            if (User != null)
            {

                var Sign = await _signIn.CheckPasswordSignInAsync(User, Model.Password, false);
                if (Sign.Succeeded)
                {
                    var Resulte = new UserDto()
                    {
                        DisplayName = User.DisplayName,
                        Token = await tokenServices.CreateTokenAsync(User, _userManager),
                        Email = User.Email
                    };
                    return Ok(Resulte);
                }
                return Unauthorized(new ApiResponse(401));


            }
            return Unauthorized(new ApiResponse(401));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto Model)
        {



            var User = new AppUser()
            {
                Email = Model.Email,
                DisplayName = Model.DisplayName,
                UserName = Model.Email.Split('@')[0],
            };
            var Resulte = await _userManager.CreateAsync(User,Model.Password);
            if (Resulte.Succeeded)
            {
                var UserDtos = new UserDto()
                {
                    Email = Model.Email,
                    DisplayName = Model.DisplayName,
                    Token = await tokenServices.CreateTokenAsync(User, _userManager)
                };
                return Ok(UserDtos);

            }
            else
                return Unauthorized(new ApiResponse(400));


        }

        [Authorize]
        [HttpGet]


        public async Task<ActionResult<UserDto>> GetUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Account = await _userManager.FindByEmailAsync(Email);

            var Resulte = new UserDto()
            {
                DisplayName = Account.DisplayName,
                Email = Email,
                Token = await tokenServices.CreateTokenAsync(Account, _userManager),
            };


            return Ok(Resulte);
        }






    } //Class

}//NameSpace
