using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JoinUs.Models
{    
    /// <summary>
    /// Model class for Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Customer user_id
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Customer latitude
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Customer longitude
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
