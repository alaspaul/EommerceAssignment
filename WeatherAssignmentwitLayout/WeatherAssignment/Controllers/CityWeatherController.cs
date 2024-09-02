using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using WeatherAssignment.Models;

namespace WeatherAssignment.Controllers
{
    public class CityWeatherController : Controller
    {

        [Route("/")]
        public IActionResult Index()
        {
            IEnumerable <CityWeather> cityWeathers = new List<CityWeather>()
            {
                new CityWeather(){CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},

                new CityWeather(){CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},

                new CityWeather(){CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:0"),  TemperatureFahrenheit = 82},
            };

            //ViewBag.CityWeathers = cityWeathers;
            return View(cityWeathers);
        }

        [Route("/Details/{cityCode}")]
        public IActionResult Details(string cityCode) 
        {
            if (string.IsNullOrEmpty(cityCode)) 
            {
                return BadRequest("city Code should be supplied");
            }

            IEnumerable<CityWeather> cityWeathers = new List<CityWeather>()
            {
                new CityWeather(){CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},

                new CityWeather(){CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},

                new CityWeather(){CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:0"),  TemperatureFahrenheit = 82},
            };


            CityWeather cityWeather = cityWeathers.Where(temp => temp.CityUniqueCode == cityCode).FirstOrDefault();

            if(cityWeather == null)
            {
                return BadRequest("city code does not exists");
            }
            else
            {
                return View(cityWeather);
            }

        }
    }
}
