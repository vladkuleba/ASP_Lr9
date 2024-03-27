using Microsoft.AspNetCore.Mvc;
using ASP_Lr9.Models;
using System.Collections.Generic;
using ASP_Lr9.Data;
using System.Threading.Tasks;

namespace ASP_Lr9.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherScraper _weatherScraper;

        public HomeController(WeatherScraper weatherScraper)
        {
            _weatherScraper = weatherScraper;
        }

        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product{ ID = 1, Name = "Product 1", Price = 100 },
                new Product{ ID = 2, Name = "Product 2", Price = 200 }
            };

            return View(products);
        }

        public async Task<IActionResult> Weather(string city = "Lviv")  //Тут можна замінити місто на потрібне і буде отримана інформація з потрібної сторінки
        {
            var weatherInfo = await _weatherScraper.GetWeatherForCity(city);

            return View(weatherInfo);
        }
    }
}
