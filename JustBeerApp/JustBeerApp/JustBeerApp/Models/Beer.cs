using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.Models
{
    public class Beer
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameDisplay")]
        public string NameDisplay { get; set; }

        [JsonProperty("abv")]
        public string Abv { get; set; }

        [JsonProperty("glasswareId")]
        public int GlasswareId { get; set; }

        [JsonProperty("styleId")]
        public int StyleId { get; set; }

        [JsonProperty("isOrganic")]
        public string IsOrganic { get; set; }

        [JsonProperty("isRetired")]
        public string IsRetired { get; set; }

        [JsonProperty("labels")]
        public Labels Labels { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusDisplay")]
        public string StatusDisplay { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        [JsonProperty("updateDate")]
        public string UpdateDate { get; set; }

        [JsonProperty("glass")]
        public Glass Glass { get; set; }

        [JsonProperty("style")]
        public Style Style { get; set; }
    }
}
