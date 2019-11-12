using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Newtonsoft.Json;
using ThermoBet.Core.Models;

namespace ThermoBet.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;
        private IUserService _userService;

        public AuthenticationController(IConfiguration config,
                                        IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// Sign the user.
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        /// <response code="200">Sigint success with the token</response>
        /// <response code="401">Sigint Failed with the error message</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/SignInOrRegister")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SignInSuccessResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SignInErrorResponse))]
        public async Task<SignInResponse> SignInOrRegister([FromBody]UserRequest userRequest)
        {

            var user = await _userService.GetByAsync(userRequest.Username, userRequest.Password);
            if(user == null)
                user = await _userService.CreateAsync(userRequest.Username, userRequest.Password);

            return new SignInSuccessResponse
            {
                IsSucsess = true,
                TokenId = "Bearer " + GenerateJSONWebToken(user)
            };
        }

        private string GenerateJSONWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              new List<Claim>() {
                  new Claim(ClaimTypes.Sid, user.Id.ToString()),
                  new Claim(ClaimTypes.Role, "User")
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}