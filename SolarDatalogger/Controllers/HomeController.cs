using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataloggerAppV2.Models;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace DataloggerAppV2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new DataModel();
            model.LoadGranularity = "1";
            model.LoadData = "";

            for (int i = 0; i < 900; i++)
            {
                model.LoadData = (model.LoadData == "")
                ? RandomNumberBetween(1, 10).ToString()
                : RandomNumberBetween(1, 10).ToString() + "," + model.LoadData;
            }
            return View(model);
        }

        public ActionResult CsvDownload()
        {
            DataModel model = new DataModel();
            var csv = new StringBuilder();
            //stub string for testing
            var testString = "{\"tm\":\"0\",\"v1\":\"0\",\"v2\":\"0\",\"v3\":\"0\",\"tc\":\"0\"}";

            csv.Append(string.Format("Timestamp,Voltage 1,Voltage 2,Voltage 3,Temperature{0}", Environment.NewLine));

            for (int i = 0; i < 900; i++)
            {
                model = JsonConvert.DeserializeObject<DataModel>(testString);

                var newLine = string.Format("{0},{1},{2},{3},{4}{5}", model.Timestamp, model.VoltageOne,
                model.VoltageTwo, model.VoltageThree, model.Temperature, Environment.NewLine);
                csv.Append(newLine);
            }

            System.IO.File.WriteAllText("d:/SolarPanelData.csv", csv.ToString());
            FileInfo exportFile = new FileInfo("d:/SolarPanelData.csv"); //for local
            //System.IO.File.WriteAllText("C:/inetpub/wwwroot/fgcusolar/Datalogger/SolarPanelData.csv", csv.ToString());
            //FileInfo exportFile = new FileInfo("C:/inetpub/wwwroot/fgcusolar/Datalogger/SolarPanelData.csv"); //for rock

            return File(exportFile.FullName, "text/csv", string.Format("SolarPanelData.csv"));
        }

        public string DataChange(string dataType)
        {
            string cheese = "";

            for (int i = 0; i < 900; i++)
            {
                cheese = (cheese == "")
                ? RandomNumberBetween(1, 10).ToString()
                : RandomNumberBetween(1, 10).ToString() + "," + cheese;
            }

            return cheese;
        }
        public string UpdateChart(string data)
        {
            string newValues = "";
            for (int i = 0; i < 8; i++)
            {
                newValues = (i == 7) ? newValues + "\"" + RandomNumberBetween(1, 10).ToString() + "\""
                    : newValues + "\"" + RandomNumberBetween(1, 10).ToString() + "\", ";
            }

            return newValues;
        }

        //stub for testing. Creates a random number within the given range.
        public Random random = new Random();
        public double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();
            return minValue + (next * (maxValue - minValue));
        }
    }
}


