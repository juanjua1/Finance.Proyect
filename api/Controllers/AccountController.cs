using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService; 
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)

        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logingDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == LoginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid Username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, logingDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var AppUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userMabager.CreateAsync(AppUser, registerDto.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userMabager.AddToRoleAsync(AppUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = AppUser.UserName,
                                Email = AppUser.Email,
                                Token = _tokenService.CreateToken(AppUser)
                            }
                        );
                    }
                    else{
                        return StatusCode(500, roleResult.Errors); 
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            } 
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}