using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Exceptions;
using RealEstate.Domain.Interfaces.Services;
using RealEstate.Presentation.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RealEstate.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountsController(IConfiguration configuration,
            IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                byte[] photo = null!;
                if (register.Photo != null)
                {
                    if (register.Photo.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await register.Photo.CopyToAsync(stream);
                            photo = stream.ToArray();
                        }
                    }
                }

                await _accountService.Register(photo, new Owner
                {
                    Address = register.Address,
                    Birthday = register.Birthday,
                    Email = register.Email,
                    Name = register.Name,
                    UserName = register.Email,
                }, register.PasswordConfirm);

                return StatusCode((int)HttpStatusCode.Created, "Owner successfully registered!!!");
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _accountService.LoginAsync(login.Email, login.Password);
                return Ok(BuildToken(user));
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private TokenDTO BuildToken(Owner user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, "Owner"),
                new Claim("Name", user.Name),
                new Claim("Address", user.Address),
                new Claim("Birthday", user.Birthday.ToString()),
                new Claim("Photo", user.Photo ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
