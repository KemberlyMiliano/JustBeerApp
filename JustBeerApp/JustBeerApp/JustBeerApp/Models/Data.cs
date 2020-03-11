using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.Models
{
    public class Data
    {

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Beer Beer { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
