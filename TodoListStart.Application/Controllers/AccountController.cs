using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoListStart.Application.Models.Auth;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidationService _validator;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IValidationService validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validator = validator;
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Create([FromBody]RegistrationUser userInfo)
        {
            var errors = _validator.ValidateRegistrationUser(userInfo);
            if (errors.Count > 0)
            {
                return BadRequest(errors); ;
            }

            var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            if (result.Errors.Count() > 0)
            {
                errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }
            
            throw new ApplicationException("Unknow registration error");
        }
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserSignIn userSignIn)
        {
            var result = await _signInManager.PasswordSignInAsync(userSignIn.Email, userSignIn.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Auth Error");
            }
        }
        [HttpGet("signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
