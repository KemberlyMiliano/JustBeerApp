using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp
{
    public static class Config
    {
        public const string NoInternetAlertMessage = "No Internet. Connect to wi-fi or cellular network";
        public const string NullMessage = null;
        public const string CancelMessage = "OK";
        public const string ApiUrl = "https://sandbox-api.brewerydb.com/v2/beer/";
        public const string ApiKey = "?key=0eca4558813950961ce12aec376f2517";
        public const string ApiRandomUrl = "https://sandbox-api.brewerydb.com/v2/beer/random/";
        public const string ApiListOfBeersUrl = "https://sandbox-api.brewerydb.com/v2/beers/";
    }
}
