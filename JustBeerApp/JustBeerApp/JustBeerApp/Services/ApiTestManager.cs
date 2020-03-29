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

        public async Task<IEnumerable<Beers>> GetBeersAsync()
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: "https://sandbox-api.brewerydb.com/v2/beers/?key=0eca4558813950961ce12aec376f2517"))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    return Barrel.Current.Get<IEnumerable<Beers>>(key: "https://sandbox-api.brewerydb.com/v2/beers/?key=0eca4558813950961ce12aec376f2517");
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync("https://sandbox-api.brewerydb.com/v2/beers/?key=0eca4558813950961ce12aec376f2517");
                var Beers = JsonConvert.DeserializeObject<IEnumerable<Beers>>(json);
                //Saves the cache and pass it a timespan for expiration
                Barrel.Current.Add(key: "https://sandbox-api.brewerydb.com/v2/beers/?key=0eca4558813950961ce12aec376f2517", data: Beers, expireIn: TimeSpan.FromDays(1));

                return Beers;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
                await App.Current.MainPage.DisplayAlert("error", $"{ex}", "ok");
            }
            return null;
        
        }

        
    }
}
