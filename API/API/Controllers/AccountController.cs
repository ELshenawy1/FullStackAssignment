using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
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
                    var value = await tokenSerivce.CreateTokenAsync(userModel);
                    return value;
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
                    var result = await tokenSerivce.CreateTokenAsync(user);
                    return result;
                }
            }
            return BadRequest("Invalid Username and Password");
        }


        [HttpGet("refreshtoken")]
        public async Task<ActionResult> RefreshToken(string refreshToken) 
        {
            var result = await tokenSerivce.RefreshTokenAsync(refreshToken);
            if(result == null) { return BadRequest(); }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is not null) userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserName(UpdateUserNameDTO updatedUser)
        {
            var user = await userManager.FindByEmailAsync(updatedUser.Email);

            if (user is not null)
            {
                user.UserName = updatedUser.Name;
                await userManager.UpdateAsync(user);
            }
            return Ok();
        }
    }
}
