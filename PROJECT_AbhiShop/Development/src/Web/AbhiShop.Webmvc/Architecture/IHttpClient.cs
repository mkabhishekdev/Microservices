using System.Net.Http;
using System.Threading.Tasks;

namespace AbhiShop.Webmvc.Architecture
{
    // to get microservice method paths & data to convey remote microserices 
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri);

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);

        Task<HttpResponseMessage> DeleteAsync(string uri);

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item);     
    }
}
