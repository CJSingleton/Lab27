using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab27.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://forecast.weather.gov/MapClick.php?lat=42.219418&lon=-83.153663&FcstType=json");
            apiRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

            if (apiResponse.StatusCode == HttpStatusCode.OK) // (== 200) if we get status of 200, things are good.
            {
                // get data and parse it
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());// use System.IO
                string weather = responseData.ReadToEnd(); //reads data from the response

                //parse the JSON data
                JObject jsonWeather = JObject.Parse(weather);
                
                ViewBag.Weather = jsonWeather["data"]["text"];
                ViewBag.WeatherImg = jsonWeather["data"]["iconLink"];
                ViewBag.WeatherTime = jsonWeather["time"]["startPeriodName"];
                
            }
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}