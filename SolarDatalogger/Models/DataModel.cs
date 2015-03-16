using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace DataloggerAppV2.Models
{
    public class DataModel
    {
        public DateTimeOffset InsertTime { get; set; }

        [JsonProperty("tm")]
        public int Timestamp { get; set; }

        [JsonProperty("v1")]
        public double VoltageOne { get; set; }

        [JsonProperty("v2")]
        public double VoltageTwo { get; set; }

        [JsonProperty("v3")]
        public double VoltageThree { get; set; }

        [JsonProperty("tc")]
        public double Temperature { get; set; }

        public string LoadData { get; set; }

        public string LoadGranularity { get; set; }
    }
}