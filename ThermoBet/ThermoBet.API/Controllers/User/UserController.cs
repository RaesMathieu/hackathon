using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ThermoBet.API.Controllers.User;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using System.Net;
using System;

namespace ThermoBet.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user detail
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/user")]
        [Authorize(Roles = "User")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<ActionResult<UserResponse>> Get()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
                var user = await _userService.GetByAsync(userId);

                return Ok(new UserResponse
                {
                    Id = user.Id,
                    UserName = user.Login,
                    Avatar = user.Avatar,
                    Pseudo = user.Pseudo
                });
            } 
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update user detail
        /// </summary>
        /// <returns></returns>
        [HttpPatch("api/user/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<ActionResult<UserResponse>> Update(
            [FromBody] JsonPatchDocument<UserRequest> patchDoc)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);

                var user = await _userService.GetByAsync(userId);
                var result = _mapper.Map<UserRequest>(user);
                patchDoc.ApplyTo(result, ModelState);

                _mapper.Map(result, user);
                await _userService.UpdateAsync(user);

                return Ok(new UserResponse
                {
                    Id = user.Id,
                    UserName = user.Login,
                    Avatar = user.Avatar,
                    Pseudo = user.Pseudo
                });
            } catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Update user detail
        /// </summary>
        /// <returns></returns>
        [HttpPatch("api/user/forAndroid/")]
        [Authorize(Roles = "User")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponse))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesErrorResponseType(typeof(void))]
        public async Task<ActionResult<UserResponse>> Update(UserRequest userRequest)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);

                var user = await _userService.GetByAsync(userId);

                if (!string.IsNullOrEmpty(userRequest.Avatar))
                    user.Avatar = userRequest.Avatar;

                if (!string.IsNullOrEmpty(userRequest.Pseudo))
                    user.Pseudo = userRequest.Pseudo;

                await _userService.UpdateAsync(user);

                return Ok(new UserResponse
                {
                    Id = user.Id,
                    UserName = user.Login,
                    Avatar = user.Avatar,
                    Pseudo = user.Pseudo
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}