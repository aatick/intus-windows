using IntusWindows.DAL.DataModels;
using IntusWindows.Shared;
using System.Net.Http.Json;

namespace IntusWindows.BLL.Services
{
    public class ClientWindowService : IClientWindowService
    {
        private readonly HttpClient _http;
        public ClientWindowService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Window>> GetWindows()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Window>>("api/windows");
                if (result == null)
                {
                    return new List<Window>();
                }
                return result;
            }
            catch (Exception)
            {
                return new List<Window>();
            }
        }
        public async Task<List<Window>> GetWindowsByOrderId(int orderId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Window>>($"api/windows/filterbyorder/{orderId}");
                if (result == null)
                {
                    return new List<Window>();
                }
                return result;
            }
            catch (Exception)
            {
                return new List<Window>();
            }
        }
        public async Task<Window> GetWindowById(int id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<Window>($"api/windows/{id}");
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<string> CreateWindow(Window window)
        {
            try
            {
                var result = await _http.PostAsJsonAsync($"api/windows/new", window);
                return "Window save successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> UpdateWindow(Window window)
        {
            try
            {
                var result = await _http.PutAsJsonAsync($"api/windows", window);
                var r = result.Content.ToString();
                return "Window updated successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> DeleteWindowById(int id)
        {
            try
            {
                var result = await _http.DeleteAsync($"api/windows/{id}");
                return "Window deleted successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
    }
}
