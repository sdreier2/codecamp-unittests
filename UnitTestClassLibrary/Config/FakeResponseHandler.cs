using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestClassLibrary
{
    public class FakeResponseHandler : DelegatingHandler
    {
        private readonly IDictionary<Uri, HttpResponseMessage> _fakeResponses = new Dictionary<Uri, HttpResponseMessage>();

        public void AddFakeResponse(Uri uri, HttpResponseMessage responseMessage, string content = "")
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                responseMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }
            _fakeResponses.Add(uri, responseMessage);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_fakeResponses.ContainsKey(request.RequestUri))
            {
                return _fakeResponses[request.RequestUri];
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request };
        }
    }
}