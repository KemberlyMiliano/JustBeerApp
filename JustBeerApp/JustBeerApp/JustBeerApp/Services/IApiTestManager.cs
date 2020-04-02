using JustBeerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.Services
{
    public interface IApiTestManager
    {
        Task<Data>GetBeersAsync(string ID);
        Task<Beer> ShowDataAsync();
    }
}
