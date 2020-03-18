using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.ViewModels
{
    public class RandomBeerPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public DelegateCommand GetRandomBeer { get; set; }
        public DelegateCommand GoToRandomBeerDetailedPage { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public bool IsBusy { get; set; }
        public string LongText { get; set; }
        public RandomBeerPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            LongText = "Lorem ipsum dolor sit amet, et qui noster dolorum, ea oblique maiorum nusquam his. Melius regione cu per. Ei mei sententiae percipitur. Has noster abhorreant no. Mel vidisse aliquip ei.";
            IsBusy = true;

            GetRandomBeer = new DelegateCommand(async () =>
            {
                await GetBeerData();

            });

            GetRandomBeer.Execute();

            GoToRandomBeerDetailedPage = new DelegateCommand(async () =>
            {
                await navigation.NavigateAsync(NavigationConstants.RandomBeerDetailedPage);

            });

            GoBackCommand = new DelegateCommand(async () =>
            {
                await navigation.GoBackAsync();


            });

        }

    }

}



