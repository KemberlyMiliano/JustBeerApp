using Acr.UserDialogs;
using JustBeerApp.Models;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.Services
{
    public class ApiTestManager : IApiTestManager
    {
        public ObservableCollection<Beer> GetData { get; set; } = new ObservableCollection<Beer>();
        public Beer Remove { get; set; } = new Beer();
        public ApiTestManager()
        {
            Barrel.ApplicationId = "CachingDataTest";
        }

        public async Task<ObservableCollection<Beer>> GetBeersAsync(string ID)
        {
            byte p = 0;
            byte n = 0;
            byte s = 0;
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: Config.ApiUrl + Config.ApiKey))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    do
                    {

                        string m;
                        m = n.ToString();
                        var rata = Barrel.Current.Get<Data>(key: m);
                        if (rata != null)
                            GetData.Add(rata.Beer);
                        else
                            s++;
                        n++;
                    } while (s != 1);
                    return GetData;
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync(Config.ApiUrl + ID + Config.ApiKey);
                var Beers = JsonConvert.DeserializeObject<Data>(json);

                do
                {
                    string m;
                    m = n.ToString();
                    var data = Barrel.Current.Get<Data>(key: m);
                    if (data == null)
                    {
                        Barrel.Current.Add(key: m, data: Beers, expireIn: TimeSpan.FromDays(1));
                        p++;
                    }
                    n++;
                } while (p != 1);
                return  null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
            }
            return null;

        }

        public async Task<ObservableCollection<Beer>> ShowDataAsync()
        {
            byte s = 0;
            byte n = 0;
            do
            {

                string m;
                m = n.ToString();
                var rata = Barrel.Current.Get<Data>(key: m);
                if (rata != null)
                    GetData.Add(rata.Beer);
                else
                    s++;
                n++;
            } while (s != 1);
            return GetData;
        }

        public void RemoveData(Beer beerparam)
        {
            byte n = 0;
            byte s = 0;
            do
            {
                string m;
                m = n.ToString();
                var rata = Barrel.Current.Get<Data>(key: m);
                Remove = rata.Beer;
                if (Remove.Id == beerparam.Id)
                {
                    Barrel.Current.Empty(key: m);
                    s++;
                }
                n++;
            } while (s != 1);
        }
    }
}
