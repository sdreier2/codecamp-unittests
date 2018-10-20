using Autofac.Extras.Moq;
using ClassLibrary1;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestClassLibrary
{
    public class ExternalWeatherApiTests
    {
        #region MockData
        private readonly WeatherForecastData _sampleData = new WeatherForecastData
        {
            Today = new TodayForecast
            {
                DayOfWeek = "Saturday",
                Date = "10/29",
                PrecipitationPercent = 0.75M,
                High = 88,
                Low = 71,
                IconUri = "/images/icon.png",
                FeelsLikeTemp = 90,
                UVIndex = "Moderate",
                SunRiseTime = "6:55 AM",
                SunsetTime = "7:42 AM"
            },
            Upcoming = new List<BasicForecast>
            {
                new BasicForecast
                {
                    DayOfWeek = "Sunday",
                    Date = "10/30",
                    PrecipitationPercent = 0.65M,
                    High = 83,
                    Low = 70,
                    IconUri = "/images/icon.png",
                },
                new BasicForecast
                {
                    DayOfWeek = "Monday",
                    Date = "10/31",
                    PrecipitationPercent = 0.65M,
                    High = 83,
                    Low = 70,
                    IconUri = "/images/icon.png",
                },
                new BasicForecast
                {
                    DayOfWeek = "Tuesday",
                    Date = "11/1",
                    PrecipitationPercent = 0.04M,
                    High = 71,
                    Low = 51,
                    IconUri = "/images/icon.png",
                },
                new BasicForecast
                {
                    DayOfWeek = "Wednesday",
                    Date = "11/2",
                    PrecipitationPercent = 0.02M,
                    High = 77,
                    Low = 56,
                    IconUri = "/images/icon.png",
                },
                new BasicForecast
                {
                    DayOfWeek = "Thursday",
                    Date = "11/3",
                    PrecipitationPercent = 0.07M,
                    High = 83,
                    Low = 67,
                    IconUri = "/images/icon.png",
                },
                new BasicForecast
                {
                    DayOfWeek = "Friday",
                    Date = "11/4",
                    PrecipitationPercent = 0.55M,
                    High = 81,
                    Low = 74,
                    IconUri = "/images/icon.png",
                }
            }
        };
        #endregion MockData

        #region Test9
        [Fact]
        public async Task Test9()
        {
            int maxDays = 4;
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IConfigurationManager>()
                    .Setup(cm => cm.GetAppSetting(It.Is<string>(s => s == "ExternalURI")))
                    .Returns("http://example.org/test/");

                FakeResponseHandler fakeResponseHandler = new FakeResponseHandler();
                fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test/"),
                    new HttpResponseMessage(HttpStatusCode.OK),
                    JsonConvert.SerializeObject(_sampleData));

                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                mock.Provide(httpClient);

                ExternalWeatherApi api = mock.Create<ExternalWeatherApi>();

                WeatherForecastData forecast = await api.GetForecast(maxDays);

                Assert.NotNull(forecast);
                Assert.NotNull(forecast.Today);
                Assert.NotEmpty(forecast.Upcoming);
                Assert.True(forecast.Upcoming.Count() <= maxDays);

                mock.Mock<IConfigurationManager>()
                    .Verify(cm => cm.GetAppSetting("ExternalURI"), Times.Once);
            }
        }
        #endregion Test9

        #region Test10
        [Fact]
        public async Task Test10()
        {
            int maxDays = 4;
            using (AutoMock mock = AutoMock.GetStrict())
            {
                mock.Mock<IConfigurationManager>()
                    .Setup(cm => cm.GetAppSetting(It.Is<string>(s => s == "ExternalURI")))
                    .Returns("http://example.org/test/");

                FakeResponseHandler fakeResponseHandler = new FakeResponseHandler();
                fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test/"),
                    new HttpResponseMessage(HttpStatusCode.OK),
                    JsonConvert.SerializeObject(_sampleData));

                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                mock.Provide(httpClient);

                ExternalWeatherApi api = mock.Create<ExternalWeatherApi>();

                WeatherForecastData forecast = await api.GetForecastAndUpdateLastRun(maxDays);

                Assert.NotNull(forecast);
                Assert.NotNull(forecast.Today);
                Assert.NotEmpty(forecast.Upcoming);
                Assert.True(forecast.Upcoming.Count() <= maxDays);

                mock.Mock<IConfigurationManager>()
                    .Verify(cm => cm.GetAppSetting("ExternalURI"), Times.Once);
            }
        }
        #endregion Test10

        #region Test11
        [Fact]
        public async Task Test11()
        {
            int maxDays = 4;
            using (AutoMock mock = AutoMock.GetStrict())
            {
                mock.Mock<IConfigurationManager>()
                    .Setup(cm => cm.GetAppSetting(It.Is<string>(s => s == "ExternalURI")))
                    .Returns("http://example.org/test/");

                mock.Mock<IConfigurationManager>()
                    .Setup(cm => cm.UpdateAppSetting(It.IsAny<string>(), It.IsAny<string>()));

                FakeResponseHandler fakeResponseHandler = new FakeResponseHandler();
                fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test/"),
                    new HttpResponseMessage(HttpStatusCode.OK),
                    JsonConvert.SerializeObject(_sampleData));

                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                mock.Provide(httpClient);

                ExternalWeatherApi api = mock.Create<ExternalWeatherApi>();

                WeatherForecastData forecast = await api.GetForecastAndUpdateLastRun(maxDays);

                Assert.NotNull(forecast);
                Assert.NotNull(forecast.Today);
                Assert.NotEmpty(forecast.Upcoming);
                Assert.True(forecast.Upcoming.Count() <= maxDays);

                mock.Mock<IConfigurationManager>()
                    .Verify(cm => cm.GetAppSetting("ExternalURI"), Times.Once);

                mock.Mock<IConfigurationManager>()
                    .Verify(cm => cm.UpdateAppSetting("LastRun", It.IsAny<string>()), Times.Once);
            }
        }
        #endregion Test11
    }
}