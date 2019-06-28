using Arena42.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Arena42.Services
{
    public class SportWebApiService
    {
        public async Task<IEnumerable<Market>> GetMarkets()
        {
            string token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://globalapi.begmedia.com/");
                var response = await client.PostAsJsonAsync("api/pub/handshake", new {
                    channel= "BetclicComIphoneApp",
                    universe= "Sports",
                    languageCode= "Fr",
                    sitecode= "FrFr"
                });
                var deserializedResponse = await response.Content.ReadAsJsonAsync<GlobalTokenResponse>();
                token = deserializedResponse.Token;
            }

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://sportapi.begmedia.com/api/pub/v3/");
                //client.
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var response = await client.GetAsync("events");
            }

            return null;
        }
    }
}