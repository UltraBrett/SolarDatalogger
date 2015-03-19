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
        private InsertDataController idc = new InsertDataController();

        public ActionResult Index()
        {
            var model = new PageLoadModel();
            var data = db.SolarDatas.ToArray();
            model.LoadGranularity = "1";
            model.LoadData = "";

            for (int i = data.Length - 1; i > data.Length - 901; i--)
            {
                model.LoadData = (model.LoadData == "")
                ? data[i].VoltageOne.ToString()
                : data[i].VoltageOne.ToString() + "," + model.LoadData;
            }
            return View(model);
        }

        public ActionResult CsvDownload(int fromTime, int toTime)
        {
            var csv = new StringBuilder();

            csv.Append(string.Format("Timestamp,Voltage 1,Voltage 2,Voltage 3,Temperature{0}", Environment.NewLine));

            using (var ent = new SolarPanelDataEntities())
            {
                var dataRange = ent.SolarDatas.SqlQuery("select * from SolarData where TimeInserted between " + fromTime + " and " + toTime).ToArray<SolarData>();
                foreach (var val in dataRange)
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4}{5}", val.TimeInserted, val.VoltageOne,
                    val.VoltageTwo, val.VoltageThree, val.Temperature, Environment.NewLine);
                    csv.Append(newLine);
                }
            }

            System.IO.File.WriteAllText("d:/SolarPanelData.csv", csv.ToString());
            FileInfo exportFile = new FileInfo("d:/SolarPanelData.csv"); //for local

            return File(exportFile.FullName, "text/csv", string.Format("SolarPanelData.csv"));
        }

        public string DataChange(string dataType)
        {
            string cheese = "";
            var data = db.SolarDatas.ToArray();

            for (int i = data.Length - 1; i > data.Length - 901; i--)
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
            for (int i = data.Length - 9; i < data.Length - 1; i++)
            {
                switch (dataType)
                {
                    case "v1":
                        newValues = (i == data.Length - 2)
                        ? newValues + "\"" + data[i].VoltageOne.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageOne.ToString() + "\", ";
                        break;
                    case "v2":
                        newValues = (i == data.Length - 2)
                        ? newValues + "\"" + data[i].VoltageTwo.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageTwo.ToString() + "\", ";
                        break;
                    case "v3":
                        newValues = (i == data.Length - 2)
                        ? newValues + "\"" + data[i].VoltageThree.ToString() + "\""
                        : newValues + "\"" + data[i].VoltageThree.ToString() + "\", ";
                        break;
                    case "temp":
                        newValues = (i == data.Length - 2)
                        ? newValues + "\"" + data[i].Temperature.ToString() + "\""
                        : newValues + "\"" + data[i].Temperature.ToString() + "\", ";
                        break;
                }
            }

            return newValues;
        }

        public string DataRange(string dataType, int fromTime, int toTime)
        {
            string cheese = "";
            using (var ent = new SolarPanelDataEntities())
            {
                var dataRange = ent.SolarDatas.SqlQuery("select * from SolarData where TimeInserted between " + fromTime + " and " + toTime).ToArray<SolarData>();
                foreach (var val in dataRange)
                {
                    switch (dataType)
                    {
                        case "v1":
                            cheese = (cheese == "")
                            ? val.VoltageOne.ToString()
                            : cheese + "," + val.VoltageOne.ToString();
                            break;
                        case "v2":
                            cheese = (cheese == "")
                            ? val.VoltageTwo.ToString()
                            : cheese + "," + val.VoltageTwo.ToString();
                            break;
                        case "v3":
                            cheese = (cheese == "")
                            ? val.VoltageThree.ToString()
                            : cheese + "," + val.VoltageThree.ToString();
                            break;
                        case "temp":
                            cheese = (cheese == "")
                            ? val.Temperature.ToString()
                            : cheese + "," + val.Temperature.ToString();
                            break;
                    }
                }
            }
            return cheese;
        }

        public void InsertData(int time, float v1, float v2, float v3, float temp)
        {   
            SolarData sd = new SolarData();
            var t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var timeStamp = t.TotalMilliseconds;
            sd.VoltageOne = v1;
            sd.VoltageTwo = v2;
            sd.VoltageThree = v3;
            sd.Temperature = temp;
            sd.TimeInserted = time;
            idc.PostSolarData(sd);
        }
    }
}


