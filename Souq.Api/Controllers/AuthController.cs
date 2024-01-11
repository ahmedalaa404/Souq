using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Souq.Api.DTOS;
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
        private readonly IMapper _Maper;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> SignIn ,ITokenServices TokenServices,IMapper Map)
        {
            _userManager = userManager;
            _signIn = SignIn;
            tokenServices = TokenServices;
            _Maper = Map;
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


            if(!CheckEmailExists(Model.Email).Result.Value)
            {
                var User = new AppUser()
                {
                    Email = Model.Email,
                    DisplayName = Model.DisplayName,
                    UserName = Model.Email.Split('@')[0],
                };
                var Resulte = await _userManager.CreateAsync(User, Model.Password);
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
            else
            {
                return BadRequest(new ApiResponse() { Message = "This Email Is already Use Before , Must Change it " });
            }


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


        #region Get Address

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.Users.Where(x => x.Email == Email).Include(x => x.Address).FirstOrDefaultAsync();
            var AddressDto = _Maper.Map<Address, AddressDto>(user.Address);
            return Ok(AddressDto);
        }
        #endregion

        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto NewAddress)
        {
            
            var EmailUser = User.FindFirstValue(ClaimTypes.Email);
            var user = await  _userManager.Users.Where(x => x.Email == EmailUser).Include(x => x.Address).FirstOrDefaultAsync();


            user.Address.FName = NewAddress.FName;
            user.Address.Lname = NewAddress.Lname;
            user.Address.Country= NewAddress.Country;
            user.Address.City= NewAddress.City;
            user.Address.Street= NewAddress.Street; 
            var Resulte=await _userManager.UpdateAsync(user);
            if (!Resulte.Succeeded) return BadRequest( new ApiResponse(400) );


            return Ok(NewAddress);
        }

            
        [HttpGet("EmailExists")]   //Hambozo@mail.com
        public async Task<ActionResult<Boolean>> CheckEmailExists(string Email)
        {
            return Ok(await _userManager.FindByEmailAsync(Email) is not null);
        }




    } //Class

}//NameSpace
