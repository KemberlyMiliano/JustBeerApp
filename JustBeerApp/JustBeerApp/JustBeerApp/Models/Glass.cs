using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.Models
{
    public class Glass
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }
    }
}
