using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.API.DTOs;
using StudentManagementSystem.Domain.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementSystem.API.Controllers
{

    [Authorize]
    [Route("[controller]")]
    public class SMSystemController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly SMSystemDomain _sMSystemDomain;

        public SMSystemController(IConfiguration _cong, SMSystemDomain sMSystemDomain)
        {
            _configuration = _cong;
            _sMSystemDomain = sMSystemDomain;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserDetails userDetails)
        {
            if (userDetails.UserName == null || userDetails.password == null || userDetails.UserName == "" || userDetails.password == "")
            {
                return BadRequest("UserName or password cannot be empty");
            }

            if (userDetails.UserName == "Admin123" && userDetails.password == "admin123") //Hardcoded for testing
            {
                int userId = 1; //For testing
                return Ok(new Dictionary<string, string> {
                    {
                        "token", CreateToken(userId)
                    }
                 });

            }
            else
            {
                return Unauthorized("Incorrect Details.");
            }
        }

        [Route("getsudents")]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var result =  await _sMSystemDomain.GetAllStudents();
            return Ok(result);  
        }


        private string CreateToken(int id)
        {
            Claim[] claims = new Claim[]{
                new Claim("userId", id.ToString()),
            };
            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:TokenKey").Value)
            );

            SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(30),
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
