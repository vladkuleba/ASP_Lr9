
using ASP_Lr9.Models;
namespace ASP_Lr9.Data
{
    public class WeatherScraper
    {
        public async Task<WeatherInfo> GetWeatherForCity(string cityName)
        {
            var weatherInfo = new WeatherInfo();
            string filePath = $"wwwroot/{cityName.ToLower()}_weather.html";

            if (!File.Exists(filePath))
            {
                return new WeatherInfo
                {
                    City = cityName,
                    Temperature = 0,
                    Description = "Файл не знайдено"
                };
            }

            string fileContent = await File.ReadAllTextAsync(filePath);

            string tempMarkerStart = "<strong>Температура:</strong>";
            int tempStart = fileContent.IndexOf(tempMarkerStart) + tempMarkerStart.Length;
            int tempEnd = fileContent.IndexOf("°C", tempStart);
            string temperature = fileContent.Substring(tempStart, tempEnd - tempStart).Trim();

            string conditionMarkerStart = "<strong>Стан:</strong>";
            int conditionStart = fileContent.IndexOf(conditionMarkerStart) + conditionMarkerStart.Length;
            int conditionEnd = fileContent.IndexOf("</p>", conditionStart);
            string condition = fileContent.Substring(conditionStart, conditionEnd - conditionStart).Trim();

            weatherInfo.City = cityName;
            weatherInfo.Temperature = int.Parse(temperature);
            weatherInfo.Description = condition;

            return weatherInfo;
        }
    }
}
