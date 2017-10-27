using Autofac.Extras.Moq;
using ClassLibrary1;
using Moq;
using System;
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
        private readonly string _sampleJson = "{\"Today\":{\"DayOfWeek\":\"Saturday\",\"Date\":\"10/29\",\"PrecipitationPercent\":0.75,\"High\":88,\"Low\":71,\"IconUri\":\"icon.png\",\"FeelsLikeTemp\":90,\"UVIndex\":\"Moderate\",\"SunRiseTime\":\"6:55 AM\",\"SunsetTime\":\"7:42 PM\"},\"Upcoming\":[{\"DayOfWeek\":\"Sunday\",\"Date\":\"10/30\",\"PrecipitationPercent\":0.65,\"High\":83,\"Low\":70,\"IconUri\":\"icon.png\"},{\"DayOfWeek\":\"Monday\",\"Date\":\"10/31\",\"PrecipitationPercent\":0.60,\"High\":81,\"Low\":57,\"IconUri\":\"icon.png\"},{\"DayOfWeek\":\"Tuesday\",\"Date\":\"11/1\",\"PrecipitationPercent\":0.04,\"High\":71,\"Low\":51,\"IconUri\":\"icon.png\"},{\"DayOfWeek\":\"Wednesday\",\"Date\":\"11/2\",\"PrecipitationPercent\":0.02,\"High\":77,\"Low\":56,\"IconUri\":\"icon.png\"},{\"DayOfWeek\":\"Thursday\",\"Date\":\"11/3\",\"PrecipitationPercent\":0.07,\"High\":83,\"Low\":67,\"IconUri\":\"icon.png\"},{\"DayOfWeek\":\"Friday\",\"Date\":\"11/4\",\"PrecipitationPercent\":0.55,\"High\":81,\"Low\":74,\"IconUri\":\"icon.png\"}]}";
        #endregion MockData

        #region Test6
        [Fact]
        public async Task Test6()
        {
            int maxDays = 4;
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IConfigurationManager>()
                    //.Setup(cm => cm.GetAppSetting("ExternalURI"))
                    .Setup(cm => cm.GetAppSetting(It.Is<string>(s => s == "ExternalURI")))
                    .Returns("http://example.org/test/");

                FakeResponseHandler fakeResponseHandler = new FakeResponseHandler();
                fakeResponseHandler.AddFakeResponse(new Uri("http://example.org/test/"),
                    new HttpResponseMessage(HttpStatusCode.OK),
                    _sampleJson);

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
        #endregion Test7

        #region Test7
        [Fact]
        public async Task Test7()
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
                    _sampleJson);

                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                mock.Provide(httpClient);

                ExternalWeatherApi api = mock.Create<ExternalWeatherApi>();

                WeatherForecastData forecast = await api.GetForeCastAndUpdateLastRun(maxDays);

                Assert.NotNull(forecast);
                Assert.NotNull(forecast.Today);
                Assert.NotEmpty(forecast.Upcoming);
                Assert.True(forecast.Upcoming.Count() <= maxDays);

                mock.Mock<IConfigurationManager>()
                    .Verify(cm => cm.GetAppSetting("ExternalURI"), Times.Once);
            }
        }
        #endregion Test7

        #region Test7Updated
        [Fact]
        public async Task Test7Updated()
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
                    _sampleJson);

                HttpClient httpClient = new HttpClient(fakeResponseHandler);
                mock.Provide(httpClient);

                ExternalWeatherApi api = mock.Create<ExternalWeatherApi>();

                WeatherForecastData forecast = await api.GetForeCastAndUpdateLastRun(maxDays);

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
        #endregion Test7Updated
    }
}