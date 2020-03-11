using JustBeerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.Services
{
    public class ApiBeerService : IApiBeerService
    {
        public async Task<Data> GetBeers(string id)
        {
            
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(Config.ApiUrl + id + Config.ApiKey);
            return JsonConvert.DeserializeObject<Data>(result);
        }

        public async Task<Data> GetRandomBeers()
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync(Config.ApiRandomUrl);
            return JsonConvert.DeserializeObject<Data>(result);
        }
    }
}
