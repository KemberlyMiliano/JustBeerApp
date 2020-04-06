using JustBeerApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.Services
{
    public interface IApiTestManager
    {
        Task<ObservableCollection<Beer>>GetBeersAsync(string ID);
        Task<ObservableCollection<Beer>> ShowDataAsync();

        void RemoveData(Beer Param);
    }
}
