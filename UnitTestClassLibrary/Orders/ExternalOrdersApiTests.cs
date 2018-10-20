using Autofac.Extras.Moq;
using ClassLibrary1;
using ClassLibrary1.Orders;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestClassLibrary.Orders
{
    public class ExternalOrdersApiTests
    {
        #region Test12
        [Fact]
        public async Task Test12()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                string name = "Strongbad";
                string email = "bob@bob.com";
                ICollection<string> itemList = new List<string>{"Item1", "Item2"};
                OrderRequestData requestData = new OrderRequestData
                {
                    Items = itemList,
                    CustomerEmail = email,
                    CustomerName = name
                };

                OrderResponseData expectedResponseData = new OrderResponseData
                {
                    DeliveryDate = "11/10/2018",
                    OrderID = 5
                };

                mock.Mock<IHttpClient>().Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Created,
                        Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
                    });

                ExternalOrdersApi service = mock.Create<ExternalOrdersApi>();

                OrderResponseData responseData = await service.SendExternalOrder(requestData);
                Assert.Equal(expectedResponseData.OrderID, responseData.OrderID);

                var serializedRequest = JsonConvert.SerializeObject(requestData);
                mock.Mock<IHttpClient>().Verify(x => x.PostAsync(It.Is<string>(s => s == ""),
                    It.Is<HttpContent>(c => c.ReadAsStringAsync().Result == serializedRequest)), Times.Once);
            }
        }
        #endregion Test12

        #region Test13
        [Fact]
        public async Task Test13()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                string name = "Strongbad";
                string email = "bob@bob.com";
                ICollection<string> itemList = new List<string>{"Item3", "Item4"};
                ICollection<string> updatedItemList = new List<string>{"Item3", "Item4", "Extra Item"};
                OrderRequestData originalRequestData = new OrderRequestData
                {
                    Items = itemList,
                    CustomerEmail = email,
                    CustomerName = name
                };

                OrderResponseData expectedResponseData = new OrderResponseData
                {
                    DeliveryDate = "11/10/2018",
                    OrderID = 5
                };

                mock.Mock<IHttpClient>().Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.Created,
                        Content = new StringContent(JsonConvert.SerializeObject(expectedResponseData), Encoding.UTF8, "application/json")
                    });

                ExternalOrdersApi service = mock.Create<ExternalOrdersApi>();

                OrderResponseData responseData = await service.SendExternalOrder(originalRequestData);
                Assert.Equal(expectedResponseData.OrderID, responseData.OrderID);

                OrderRequestData updatedRequest = new OrderRequestData
                {
                    Items = updatedItemList,
                    CustomerEmail = email,
                    CustomerName = name
                };
                var serializedRequest = JsonConvert.SerializeObject(updatedRequest);

                mock.Mock<IHttpClient>().Verify(x => x.PostAsync(It.Is<string>(s => s == ""),
                    It.Is<HttpContent>(c => c.ReadAsStringAsync().Result == serializedRequest)), Times.Once);
            }
        }
        #endregion Test13
    }
}