using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SolarDatalogger.Models
{
    public class PageLoadModel
    {
        public string LoadData { get; set; }

        public string LoadGranularity { get; set; }
    }
}