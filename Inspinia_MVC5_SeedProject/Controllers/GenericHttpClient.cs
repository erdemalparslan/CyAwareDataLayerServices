using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class GenericHttpClient<T, TResourceIdentifier> : IDisposable where T : class
    {
        private bool disposed = false;
        private HttpClient httpClient;
        protected readonly string serviceBaseAddress;
        private readonly string addressSuffix;
        private readonly string jsonMediaType = "application/json";

        public GenericHttpClient(string serviceBaseAddress, string addressSuffix)
        {
            this.serviceBaseAddress = serviceBaseAddress;
            this.addressSuffix = addressSuffix;
            this.httpClient = MakeHttpClient(serviceBaseAddress);
        }

        protected virtual HttpClient MakeHttpClient(string serviceBaseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Cihan_HttpClient", "1.0")));
            return httpClient;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpResponseMessage responseMessage = await httpClient.GetAsync(addressSuffix);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> GetByIdAsync(TResourceIdentifier identifier)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpResponseMessage responseMessage = await httpClient.GetAsync(addressSuffix + identifier.ToString());
            responseMessage.EnsureSuccessStatusCode();
            T model = await responseMessage.Content.ReadAsAsync<T>();
            return model;
        }

        public async Task<T> PostAsync(T model)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(addressSuffix, model);
            return await responseMessage.Content.ReadAsAsync<T>();
        }

        public async Task PutAsync(TResourceIdentifier identifier, T model)
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsJsonAsync(addressSuffix + identifier.ToString(), model);
        }

        public async Task DeleteAsync(TResourceIdentifier identifier)
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(addressSuffix + identifier.ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                {
                    var hc = httpClient;
                    httpClient = null;
                    hc.Dispose();
                }
                disposed = true;
            }
        }
    }
}