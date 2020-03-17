using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JustBeerApp.Models
{
    public class Beers
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        [JsonProperty("data")]
        public ObservableCollection<Datum> Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
