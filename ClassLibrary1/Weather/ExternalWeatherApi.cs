using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IExternalWeatherApi
    {
        Task<WeatherForecastData> GetForecast(int maxDays);
        Task<WeatherForecastData> GetForecastAndUpdateLastRun(int maxDays);
    }

    public class ExternalWeatherApi : IExternalWeatherApi
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly HttpClient _client;

        public ExternalWeatherApi(HttpClient client,
            IConfigurationManager configurationManager)
        {
            _client = client;
            _configurationManager = configurationManager;
        }

        public async Task<WeatherForecastData> GetForecast(int maxDays)
        {
            string uriString = _configurationManager.GetAppSetting("ExternalURI");
            HttpResponseMessage response = await _client.GetAsync(new Uri(uriString));
            string content = await response.Content.ReadAsStringAsync();

            WeatherForecastData data = JsonConvert.DeserializeObject<WeatherForecastData>(content);
            if (data.Upcoming.Count() > maxDays)
            {
                data.Upcoming = data.Upcoming.Take(maxDays);
            }
            return data;
        }

        public async Task<WeatherForecastData> GetForecastAndUpdateLastRun(int maxDays)
        {
            _configurationManager.UpdateAppSetting("LastRun", DateTime.Now.ToShortDateString());
            return await GetForecast(maxDays);
        }
    }
}