using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Arena42.Services;
using Arena42.Services.Repository;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.Swagger.Annotations;

namespace Arena42.Controllers
{
    public class AuthenticationController : ApiController
    {
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class RegisterSuccessResponse
        {
        }

        public class RegisterErrorResponse
        {
            /// <summary>
            /// Explain why the Register faild
            /// </summary>
            public string ErrorMessage { get; }
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Register successn</response>
        /// <response code="401">register Failed with the error message</response>
        /// [HttpPost]
        [Route("api/register")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RegisterSuccessResponse))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(RegisterErrorResponse))]

        public async Task<IHttpActionResult> Register(User user)
        {
            return Ok(new RegisterSuccessResponse());
        }

        public class SignInSuccessResponse
        {
            /// <summary>
            /// Token
            /// </summary>
            public string TokenId { get; set; }
        }

        public class SignInErrorResponse
        {
            /// <summary>
            /// Explain why the Sign In faild
            /// </summary>
            public string ErrorMessage { get; }
        }

        /// <summary>
        /// Sign the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Sigint success with the token</response>
        /// <response code="401">Sigint Failed with the error message</response>
        [HttpPost]
        [Route("api/signin")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SignInSuccessResponse))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(SignInErrorResponse))]
        public async Task<IHttpActionResult> SignIn(User user)
        {
            return Ok(new SignInSuccessResponse());
        }

        /// <summary>
        /// Sign the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="200">Sigint success with the token</response>
        /// <response code="401">Sigint Failed with the error message</response>
        [HttpPost]
        [Route("api/SignInOrRegister")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(SignInSuccessResponse))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(SignInErrorResponse))]
        public async Task<IHttpActionResult> SignInOrRegister(User user)
        {
            using (var db = new Adriana42Context())
            {
                var repository = new Repository<Models.User>(db);

                var usr = repository.Find(x => x.UserName == user.Username).FirstOrDefault();

                if (usr == null)
                {
                    usr = new Models.User
                    {
                        UserName = user.Username,
                        Password = user.Password
                    };
                    repository.Add(usr);
                }

                db.SaveChanges();
                return Ok(new SignInSuccessResponse
                {
                    TokenId = usr.Id.ToString()
                });

            }
        }
    }

}

