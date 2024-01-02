using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenSerivce;

        public AccountController(UserManager<User> _userManager, ITokenService _tokenSerivce)
        {
            userManager = _userManager;
            tokenSerivce = _tokenSerivce;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                User userModel = new()
                {
                    Email = user.Email,
                    UserName = user.Name
                };
                var result = await userManager.CreateAsync(userModel, user.Password);
                if (result.Succeeded)
                {
                    var res = await tokenSerivce.CreateTokenAsync(userModel);
                    return new UserDTO
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Token = res.Token,
                        RefreshToken = res.RefreshToken,
                        RefreshTokenExpiration = res.RefreshTokenExpiration,    
                    };
                }
                return BadRequest(result.Errors);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(login.Email);
                if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
                {
                    user.LastLoginTime = DateTime.Now;
                    await userManager.UpdateAsync(user);
                    var res = await tokenSerivce.CreateTokenAsync(user);
                    //if (!string.IsNullOrEmpty(res.RefreshToken))
                    //{
                    //    SetRefreshTokenInCookie(res.RefreshToken, res.RefreshTokenExpiration);
                    //}
                    return new UserDTO
                    {
                        Email = user.Email,
                        Name = user.UserName,
                        Token = res.Token,
                        TokenExpiration = res.TokenExpiration,
                        RefreshToken = res.RefreshToken,
                        RefreshTokenExpiration = res.RefreshTokenExpiration
                        
                    };
                }
            }
            return BadRequest("Invalid UserName and Password");
        }
        //private void SetRefreshTokenInCookie(string refreshToken, DateTime expires )
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = expires.ToLocalTime()
        //    };
        //    Response.Cookies.Append("refreshToken", refreshToken,cookieOptions);
        //}


        [HttpGet("refreshtoken")]
        public async Task<ActionResult> RefreshToken(string refreshToken) 
        {
            var result = await tokenSerivce.RefreshTokenAsync(refreshToken);
            if(result == null) { return BadRequest(); }

            //SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
    }
}
