using Microsoft.AspNetCore.Mvc;
using WeatherAssignment.Models;
namespace WeatherAssignament.ViewComponents
{
    public class CityWeatherViewComponent :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CityWeather city)
        {
            ViewBag.BGColor = BackgroundColor(city.TemperatureFahrenheit);

            return View(city);
        }


        public string BackgroundColor(int fahrenheit)
        {
            if (fahrenheit < 44)
            {
                return "blue-back";
            }
            else if (fahrenheit > 44 && fahrenheit < 74)
            {
                return "green-back";
            }
            else
            {
                return "orange-back";
            }
        }
    }
}
