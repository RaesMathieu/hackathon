using Arena42.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arena42.Controllers
{
    public class TournamentController : ApiController
    {
        // GET api/values/5
        [Route("api/tournaments")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Tournament>))]
        public IHttpActionResult GetTournanents()
        {
            var tournamentList = new List<Tournament>
            {
                new Tournament
                {
                    Id = 1,
                    Description = "Premier tournoi du betclic hackathon !",
                    StartTimeUtc = DateTime.UtcNow,
                    EndTimeUtc = DateTime.UtcNow,
                    ImgUrl = "http://www.footmercato.net/images/a/benjamin-lecomte-plait-beaucoup-a-monaco_257228.jpg",
                    Name = "Ligue 1 journée 1",
                    Market = new List<Market>
                    {
                    }
                }
            };
            return Ok(new List<Tournament>(tournamentList));
        }

        //// GET api/values/5
        //[Route("api/tournament/{id}")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(Tournament))]
        //public IHttpActionResult GetTournamentById(int id)
        //{
        //    return Ok(new Tournament
        //    {
        //        Id = id,
        //        StartTimeUtc = DateTime.Now,
        //        EndTimeUtc = DateTime.Now.AddDays(1)
        //    });
        //}

        //// GET api/values/5
        //[Route("api/tournament/{tournamentid}/leaderboard")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(LeaderBoard))]
        //public IHttpActionResult GetLeaderBoardByTournamentId(int tournamentid)
        //{
        //    return Ok(new LeaderBoard());
        //}

        // GET api/values/5
        [Route("api/tournament/{tournamentid}/Result")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(TournamentResult))]
        public IHttpActionResult GetResultByTournamentId(int tournamentid)
        {
            return Ok(new TournamentResult());
        }

        // POST api/values
        [Route("api/tournament/{tournamentid}/market/{marketid}/selection/{selectionid}")]
        public IHttpActionResult Post(int tournamentId, int marketId, int selectionId)
        {
            return Ok();
        }
    }
}
