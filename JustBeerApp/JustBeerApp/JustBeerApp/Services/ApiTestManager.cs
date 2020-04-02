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
    public class ApiTestManager : IApiTestManager
    {
        public ApiTestManager()
        {
            Barrel.ApplicationId = "CachingDataTest";
        }

        public async Task<Data> GetBeersAsync(string ID)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: Config.ApiUrl + Config.ApiKey))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    var data = Barrel.Current.Get<Data>(key: Config.ApiUrl + Config.ApiKey);
                    return data;
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync(Config.ApiUrl + ID + Config.ApiKey);
                var Beers = JsonConvert.DeserializeObject<Data>(json);

                Barrel.Current.Add(key: Config.ApiUrl + Config.ApiKey, data: Beers, expireIn: TimeSpan.FromDays(1));

                return  null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
            }
            return null;

        }

        public async Task<Beer> ShowDataAsync()
        {
            var data = Barrel.Current.Get<Data>(key: Config.ApiUrl + Config.ApiKey);
            return data.Beer;
        }

        public void RemoveData()
        {
            Barrel.Current.Empty(key: Config.ApiUrl + Config.ApiKey);
        }
    }
}
