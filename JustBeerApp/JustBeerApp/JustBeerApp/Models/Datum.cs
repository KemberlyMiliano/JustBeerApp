using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.Models
{
    public class Datum
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ibu")]
        public string Ibu { get; set; }

        [JsonProperty("originalGravity")]
        public string OriginalGravity { get; set; }

        [JsonProperty("availableId")]
        public int? AvailableId { get; set; }

        [JsonProperty("foodPairings")]
        public string FoodPairings { get; set; }

        [JsonProperty("available")]
        public Available Available { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("srmId")]
        public int? SrmId { get; set; }

        [JsonProperty("srm")]
        public Srm Srm { get; set; }

        [JsonProperty("servingTemperature")]
        public string ServingTemperature { get; set; }

        [JsonProperty("servingTemperatureDisplay")]
        public string ServingTemperatureDisplay { get; set; }
    }
}
