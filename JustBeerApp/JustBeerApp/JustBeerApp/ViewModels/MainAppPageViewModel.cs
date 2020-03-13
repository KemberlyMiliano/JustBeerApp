using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.ViewModels
{
    public class MainAppPageViewModel
    {
        public DelegateCommand TabNavigacion { get; set; }

        public MainAppPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            var nav = new NavigationParameters();
            TabNavigacion = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.TabbedMenu, nav);
            });
        }
    }
}

