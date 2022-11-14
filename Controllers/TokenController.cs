using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using SenwesAssignment_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace SenwesAssignment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        public readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            // _context = context;
        }


        /// <summary>
        /// Get User Token
        /// </summary>4
        /// <returns>Returns a list token based on user data that exist on user repository use  Ben@gmail.com and Ben123</returns>
        /// 
        [HttpPost]
        public IActionResult Post(UserModel userInfo)
        
        {
            if (userInfo != null && userInfo.UserName != null && userInfo.Password != null)
            {
                //UserModel = userInfo;
                IActionResult response = Unauthorized();
              
                var user = GetUser(userInfo);
                if (user != null)
                {
                    var claims = new[] {
                          new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                          new Claim("Id",user.Id.ToString()),
                          new Claim("UserName",user.UserName),
                        //  new Claim("Password",user.password)
                      };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                {
                    return BadRequest("Invalid credentials");
                }


            }
            else
            {
                return BadRequest();

            }
        }


        private UserDTO GetUser(UserModel userModel)
        {
            //Write your code here to authenticate the user
            return _userRepository.GetUser(userModel);
        }
    }
}
