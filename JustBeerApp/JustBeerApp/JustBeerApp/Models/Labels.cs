using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.Models
{
    public class Labels
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("contentAwareIcon")]
        public string ContentAwareIcon { get; set; }

        [JsonProperty("contentAwareMedium")]
        public string ContentAwareMedium { get; set; }

        [JsonProperty("contentAwareLarge")]
        public string ContentAwareLarge { get; set; }
    }
}
