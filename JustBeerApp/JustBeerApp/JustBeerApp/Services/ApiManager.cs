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
    public class ApiManager : IApiManager
    {
        public ObservableCollection<Beer> GetData { get; set; } = new ObservableCollection<Beer>();
        public Beer Remove { get; set; } = new Beer();
        public ApiManager()
        {
            Barrel.ApplicationId = "CachingData";
        }
        public async Task<ObservableCollection<Beer>> GetBeersAsync(string ID)
        {
            byte KeyNumber = 0;
            byte Condicion = 0;
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet &&
                    !Barrel.Current.IsExpired(key: Config.ApiUrl + Config.ApiKey))
                {
                    await Task.Yield();
                    UserDialogs.Instance.Toast("Please check your internet connection", TimeSpan.FromSeconds(5));
                    do
                    {

                        string KeyString;
                        KeyString = KeyNumber.ToString();
                        var result = Barrel.Current.Get<Data>(key: KeyString);
                        if (result != null)
                            GetData.Add(result.Beer);
                        else
                            Condicion++;
                        KeyNumber++;
                    } while (Condicion != 1);
                    return GetData;
                }

                var client = new HttpClient();
                var json = await client.GetStringAsync(Config.ApiUrl + ID + Config.ApiKey);
                var Beers = JsonConvert.DeserializeObject<Data>(json);

                do
                {
                    string KeyString;
                    KeyString = KeyNumber.ToString();
                    var data = Barrel.Current.Get<Data>(key: KeyString);
                    if (data == null)
                    {
                        Barrel.Current.Add(key: KeyString, data: Beers, expireIn: TimeSpan.FromDays(1));
                        Condicion++;
                    }
                    KeyNumber++;
                } while (Condicion != 1);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API EXCEPTION {ex}");
            }
            return null;

        }

        public async Task<ObservableCollection<Beer>> ShowDataAsync()
        {
            byte Condicion = 0;
            byte KeyNumber = 0;
            do
            {

                string KeyString;
                KeyString = KeyNumber.ToString();
                var result = Barrel.Current.Get<Data>(key: KeyString);
                if (result != null)
                    GetData.Add(result.Beer);
                else
                    Condicion++;
                KeyNumber++;
            } while (Condicion != 1);
            return GetData;
        }

        public void RemoveData(Beer beerparam)
        {
            byte KeyNumber = 0;
            byte Condicion = 0;
            do
            {
                string KeyString;
                KeyString = KeyNumber.ToString();
                var result = Barrel.Current.Get<Data>(key: KeyString);
                Remove = result.Beer;
                if (Remove.Id == beerparam.Id)
                {
                    Barrel.Current.Empty(key: KeyString);
                    Condicion++;
                }
                KeyNumber++;
            } while (Condicion != 1);
        }
    }
}

