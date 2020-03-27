using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.ViewModels
{
    public class MainAppPageViewModel : BaseViewModel
    {
        public DelegateCommand TabNavigacion { get; set; }
        public MainAppPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService, IApiManager apiManager) : base(navigation, apiService, pageDialogService, apiManager)
        {
            var nav = new NavigationParameters();
            TabNavigacion = new DelegateCommand(async () =>
            {
                await NavigationService.NavigateAsync(NavigationConstants.TabbedMenu, nav);
            });
        }
    }
}

