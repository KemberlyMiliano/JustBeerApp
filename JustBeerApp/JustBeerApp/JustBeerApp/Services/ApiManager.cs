using Acr.UserDialogs;
using JustBeerApp.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.Services
{
    public class ApiManager : IApiManager
    {
        public ApiManager()
        {
            Barrel.ApplicationId = "CachingDataSample";
        }
        public async Task<IEnumerable<Data>> GetBeerAsync(string id)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: Config.ApiUrl + id + Config.ApiKey))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    return Barrel.Current.Get<IEnumerable<Data>>(key: Config.ApiUrl + id + Config.ApiKey) ;
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync(Config.ApiUrl + id + Config.ApiKey);
                var Beers = JsonConvert.DeserializeObject<IEnumerable<Data>>(json);

                Barrel.Current.Add(key: Config.ApiUrl, data: Beers, expireIn: TimeSpan.FromDays(1));

                return Beers;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
            }
            return null;
        }
    }
}
