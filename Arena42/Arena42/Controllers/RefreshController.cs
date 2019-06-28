using Arena42.Services;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Arena42.Controllers
{
    public class RefreshController : ApiController
    {
        [Route("api/refresh")]
        [SwaggerResponse(HttpStatusCode.OK)]
        public async Task<IHttpActionResult> Refresh()
        {
            var service = new SportWebApiService();
            await service.GetMarkets();

            return Ok();
        }
    }
}
