using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ServiceContracts;
using System.Net;
using WeatherAssignment.Models;

namespace WeatherAssignment.Controllers
{
    public class CityWeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public CityWeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var cities = _weatherService.GetWeatherDetails();

            return View(cities);
        }

        [Route("/Details/{cityCode}")]
        public IActionResult Details(string cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                return View();
            }

            CityWeather? city = _weatherService.GetWeatherByCityCode(cityCode);

            return View(city);
        }
    }
}
