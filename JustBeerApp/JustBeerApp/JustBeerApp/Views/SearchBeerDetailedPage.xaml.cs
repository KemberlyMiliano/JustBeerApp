using JustBeerApp.Models;
using JustBeerApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JustBeerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBeerDetailedPage : ContentPage
    {
        public Beers BeerList { get; set; } = new Beers();
        public ObservableCollection<Datum> HomeBeers { get; set; } = new ObservableCollection<Datum>();
        public ApiBeerService ApiBeerService { get; set; } = new ApiBeerService();
        public SearchBeerDetailedPage()
        {
            GetBeers();
            InitializeComponent();
        }

        public async Task GetBeers()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    BeerList = await ApiBeerService.GetListOfBeers();
                    HomeBeers = BeerList.Data;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }
            }
            else
            {
                await DisplayAlert(Config.NullMessage, Config.NoInternetAlertMessage, Config.CancelMessage);
            }

        }

        private void BeersSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = BeersSearchBar.Text;
            var suggestions = HomeBeers.Where(b => b.NameDisplay.ToLower().Contains(keyword.ToLower()));
            SuggestionsListView.ItemsSource = suggestions;
        }
    }
}