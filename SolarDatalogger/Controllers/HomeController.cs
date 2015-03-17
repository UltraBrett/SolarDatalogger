using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;
using SolarDatalogger.Models;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace SolarDatalogger.Controllers
{
    public class HomeController : Controller
    {

        private SolarPanelDataEntities db = new SolarPanelDataEntities();

        public ActionResult Index()
        {
            var model = new PageLoadModel();
            var data = db.SolarDatas.ToArray();
            model.LoadGranularity = "1";
            model.LoadData = "";

            for (int i = 0; i < 900; i++)
            {
                model.LoadData = (model.LoadData == "")
                ? data[i].VoltageOne.ToString()
                : data[i].VoltageOne.ToString() + "," + model.LoadData;
            }
            return View(model);
        }

        public ActionResult CsvDownload()
        {
            var data = db.SolarDatas.ToArray();
            var csv = new StringBuilder();

            csv.Append(string.Format("Timestamp,Voltage 1,Voltage 2,Voltage 3,Temperature{0}", Environment.NewLine));

            for (int i = 0; i < 900; i++)
            {       
                var newLine = string.Format("{0},{1},{2},{3},{4}{5}", data[i].Id, data[i].VoltageOne,
                data[i].VoltageTwo, data[i].VoltageThree, data[i].Temperature, Environment.NewLine);
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
            var data = db.SolarDatas.ToArray();

            for (int i = 0; i < 900; i++)
            {
                switch (dataType)
                {
                    case "v1":
                        cheese = (cheese == "")
                        ? data[i].VoltageOne.ToString()
                        : data[i].VoltageOne.ToString() + "," + cheese;
                        break;
                    case "v2":
                        cheese = (cheese == "")
                        ? data[i].VoltageTwo.ToString()
                        : data[i].VoltageTwo.ToString() + "," + cheese;
                        break;
                    case "v3":
                        cheese = (cheese == "")
                        ? data[i].VoltageThree.ToString()
                        : data[i].VoltageThree.ToString() + "," + cheese;
                        break;
                    case "temp":
                        cheese = (cheese == "")
                        ? data[i].Temperature.ToString()
                        : data[i].Temperature.ToString() + "," + cheese;
                        break;
                }
                
            }

            return cheese;
        }

        public string UpdateChart(string dataType)
        {
            string newValues = "";
            var data = db.SolarDatas.ToArray();
            for (int i = 0; i < 8; i++)
            {
                switch (dataType)
                {
                    case "v1":
                        newValues = (i == 7)
                        ? newValues + "\"" + data[i].VoltageOne.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageOne.ToString() + "\", ";
                        break;
                    case "v2":
                        newValues = (i == 7)
                        ? newValues + "\"" + data[i].VoltageTwo.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageTwo.ToString() + "\", ";
                        break;
                    case "v3":
                        newValues = (i == 7)
                        ? newValues + "\"" + data[i].VoltageThree.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageThree.ToString() + "\", ";
                        break;
                    case "temp":
                        newValues = (i == 7)
                        ? newValues + "\"" + data[i].Temperature.ToString() + "\""
                        : newValues + "\"" + data[i].Temperature.ToString() + "\", ";
                        break;
                }
            }

            return newValues;
        }
    }
}


