using JustBeerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.Services
{
    public interface IApiManager
    {
        Task<IEnumerable<Data>> GetBeerAsync();
    }
}
