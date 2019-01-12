using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using CityInfo.Entities;
using CityInfo.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CityInfo.Controllers
{
        [Produces("application/json")]
        [Route("api/Account")]
        public class AccountController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IConfiguration _configuration;

            public AccountController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                this._configuration = configuration;
            }

            //this create a user in the identity framwork class
            [Route("Create")]
            [HttpPost]
            public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return BuildToken(model);
                    }
                    else
                    {                    
                      return BadRequest($"Username or password invalid-Errors:{result.Errors.IdentityErrorsFormat()}");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }

            [HttpPost]
            [Route("Login")]
            public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return BuildToken(userInfo);
                    }
                    else
                    {
                    
                        ModelState.AddModelError(string.Empty, $"Invalid login attempt IsNotAllow: {result.IsNotAllowed}");
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            private IActionResult BuildToken(UserInfo userInfo)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                //crear cualquier variable que queramos
                new Claim("miValor", "Lo que yo quiera"),
                //generar un identificador del token, en caso que este token este en varios dispositivos
                //se puede desloggear en todos los dispositivos
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                //set the signature
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_super_secreta"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddHours(1);

                JwtSecurityToken token = new JwtSecurityToken(
                    //the domain who emited the token
                   issuer: "yourdomain.com",
                   audience: "yourdomain.com",
                   claims: claims,
                   expires: expiration,
                   signingCredentials: creds);

                return Ok(
                new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = expiration
                });

            }

        }
    public static class IdentityErrors {
        public static string IdentityErrorsFormat(this IEnumerable<IdentityError> errors) {
            string stringFormat = string.Empty;
            if (errors !=null && errors.Count() > 0)
            {
                errors.ToList()
                .ForEach(f =>
                  { stringFormat += $" Code -{f.Code} Description - { f.Description}"; }
                );
            }
            return stringFormat;
        }
    }
}
