using System.Collections.Generic;

namespace ClassLibrary1
{
    public class WeatherForecastData
    {
        public TodayForecast Today { get; set; }
        public IEnumerable<BasicForecast> Upcoming { get; set; }
    }

    public class TodayForecast
    {
        public string DayOfWeek { get; set; }
        public string Date { get; set; }
        public decimal PrecipitationPercent { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public string IconUri { get; set; }
        public int FeelsLikeTemp { get; set; }
        public string UVIndex { get; set; }
        public string SunRiseTime { get; set; }
        public string SunsetTime { get; set; }
    }

    public class BasicForecast
    {
        public string DayOfWeek { get; set; }
        public string Date { get; set; }
        public decimal PrecipitationPercent { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public string IconUri { get; set; }
    }
}