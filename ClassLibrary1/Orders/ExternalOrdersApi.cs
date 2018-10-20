using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Orders
{
    public interface IExternalOrdersApi
    {
        Task<OrderResponseData> SendExternalOrder(OrderRequestData orderRequestData);
    }

    public class ExternalOrdersApi : IExternalOrdersApi
    {
        private readonly IHttpClient _client;

        public ExternalOrdersApi(IHttpClient client)
        {
            _client = client;
        }

        public async Task<OrderResponseData> SendExternalOrder(OrderRequestData orderRequestData)
        {
            //business logic to possibly modify the object being posted
            if (orderRequestData.Items.Any(i => i == "Item3"))
            {
                orderRequestData.Items.Add("Extra Item");
            }
            string serialziedOrder = JsonConvert.SerializeObject(orderRequestData);

            HttpResponseMessage response = await _client.PostAsync("",
                new StringContent(serialziedOrder, Encoding.UTF8, "application/json"));

            string responseContent = await response.Content.ReadAsStringAsync();
            OrderResponseData orderResponse = JsonConvert.DeserializeObject<OrderResponseData>(responseContent);

            return orderResponse;
        }
    }
}