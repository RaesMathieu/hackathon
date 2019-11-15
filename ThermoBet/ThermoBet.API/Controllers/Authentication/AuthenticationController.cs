using System;
using System.Net;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using ThermoBet.Core.Models;

namespace ThermoBet.API.Controllers
{
    [ApiController]
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
        /// <response code="401">Sigint Failed with login already exist with another password</response>
        /// <response code="500">Sigint Failed with internal server error</response>
        [HttpPost("api/SignInOrRegister")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SignInSuccessResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(SignInErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<SignInResponse>> SignInOrRegister([FromBody]SigInRequest userRequest)
        {
            try
            {
                var user = await _userService.GetByAsync(userRequest.Username, userRequest.Password);
                if (user == null)
                    user = await _userService.CreateAsync(userRequest.Username, userRequest.Password);

                if (user == null)
                    return Unauthorized(new SignInErrorResponse {
                        IsSucsess = false,
                        ErrorMessage = "User already exist."
                    });

                await _userService.SigInAsync(user);

                return Ok(new SignInSuccessResponse
                {
                    IsSucsess = true,
                    TokenId = "Bearer " + GenerateJSONWebToken(user)
                });
            } catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new SignInErrorResponse {
                    IsSucsess = false,
                    ErrorMessage  = ex.Message
                });
            }
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
              expires: DateTime.Now.AddHours(12),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}