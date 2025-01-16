using Paslauga.Entities;

namespace Paslauga.Helpers
{
    public class ApiService
    {

        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }

        public async Task<List<T>> GetAsyncList<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(url);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            return await _httpClient.PostAsJsonAsync(url, data);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _httpClient.DeleteAsync(url);
        }

#region "VDCS"

        public async Task<List<VDC>> GetVDCsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<VDC>>("list/vdcs");
            return response ?? new List<VDC>();
        }
#endregion

#region "Organisations"

        public async Task<Organisation> GetORGAsync(int? id)
        {
            var response = await _httpClient.GetFromJsonAsync<Organisation>($"find/organisation/{id}");
            return response ?? new Organisation();
        }
#endregion
    }
}
