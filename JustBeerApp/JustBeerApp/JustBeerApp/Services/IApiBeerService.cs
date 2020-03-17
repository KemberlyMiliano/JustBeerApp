using JustBeerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.Services
{
    public interface IApiBeerService
    {
        Task<Data> GetBeers(string id);
        Task<Data> GetRandomBeers();
        Task<Beers> GetListOfBeers();
    }
}
