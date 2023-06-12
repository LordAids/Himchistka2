using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Himchistka.Data.Identity;
using Himchistka.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Himchistka.Api.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Himchistka.Api.JWT;
using Microsoft.EntityFrameworkCore;

namespace Himchistka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
               _context = db;
               _userManager = userManager;
               _roleManager = roleManager;
        }


        /// <summary>
        /// Get token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] LoginViewModel model)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user != null)
                {
                    var pass = await _userManager.CheckPasswordAsync(user, model.Password);
                    if(pass)
                    {
                        var userClaims = await _userManager.GetClaimsAsync(user);
                        var roles = await _userManager.GetRolesAsync(user);
                        var claims = new List<Claim>
                        {
                                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                        };
                        
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                        }

                        ClaimsIdentity claimsIdentity =
                                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                    ClaimsIdentity.DefaultRoleClaimType);

                        // создаем JWT-токен
                        var now = DateTime.UtcNow;
                        var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            notBefore: now,
                            claims: claimsIdentity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                                SecurityAlgorithms.HmacSha256));
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        var response = new
                        {
                            access_token = encodedJwt,
                            username = claimsIdentity.Name,
                            roles = _userManager.GetRolesAsync(user).Result,
                            start = now,
                            finish = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
                        };
                        return Ok(response);
                    }
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] RegisterViewModel model)
        {
            if(!ModelState.IsValid) return BadRequest();

            if(model.Id != null)
            {
                var userPlaces = await _context.Places.Where(p => model.Places.Contains(p.Id)).ToListAsync();

                var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == model.Id);
                if(user != null)
                {
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.Places = userPlaces;
                }
                return Ok();
            }
            
            var newUserPlaces = await _context.Places.Where(p => model.Places.Contains(p.Id)).ToListAsync();

            ApplicationUser newUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                Places = newUserPlaces
            };
            await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, "Employee");
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            var employee = _context.ApplicationUsers.Include(p => p.Places).Where(a => a.UserName != "sadmin").ToList();
            foreach (var em in employee)
                foreach (var places in em.Places)
                    places.Employee = new List<ApplicationUser>();

            return Ok(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id.ToString());
            if(user != null) 
                await _userManager.DeleteAsync(user);

            return Ok();
        }

    }
}
