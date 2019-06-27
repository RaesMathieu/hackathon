using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
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
    }

}
