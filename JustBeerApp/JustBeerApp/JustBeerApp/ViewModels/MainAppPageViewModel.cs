using JustBeerApp.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
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
        public MainAppPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            TabNavigacion = new DelegateCommand(async () =>
            {
                var request = new AuthenticationRequestConfiguration("Use your fingerprint, We need to verify your humanity!");
                var result = await CrossFingerprint.Current.AuthenticateAsync(request);
                if (result.Authenticated)
                {
                    await NavigationService.NavigateAsync(NavigationConstants.TabbedMenu);
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync("Results are here", "Invalid fingerprint", "Ok");
                }
            });
        }
    }
}

