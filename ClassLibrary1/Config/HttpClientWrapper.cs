using System.Net.Http;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string uri);

        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);

        Task<HttpResponseMessage> PutAsync(string uri, HttpContent content);

        Task<HttpResponseMessage> DeleteAsync(string uri);
    }

    public class HttpClientWrapper : IHttpClient
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            return await _client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            return await _client.PostAsync(uri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string uri, HttpContent content)
        {
            return await _client.PutAsync(uri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
            return await _client.DeleteAsync(uri);
        }
    }
}