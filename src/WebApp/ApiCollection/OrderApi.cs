using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.ApiCollection.Infrastructure;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;
using WebApp.Settings;

namespace WebApp.ApiCollection
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory, IApiSettings settings) : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string username)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
             .SetPath(_settings.OrderPath)
             .AddQueryString("username", username)
             .HttpMethod(HttpMethod.Get)
             .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderResponseModel>>(message);
        }

    }
}
