using IntusWindows.DAL.DataModels;
using IntusWindows.Shared;
using System.Net.Http.Json;

namespace IntusWindows.BLL.Services
{
    public class ClientSubElementService : IClientSubElementService
    {
        private readonly HttpClient _http;
        public ClientSubElementService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<SubElement>> GetSubElements()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<SubElement>>("api/subelements");
                if (result == null)
                {
                    return new List<SubElement>();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<SubElement>();
            }
        }
        public async Task<List<SubElement>> GetSubElementsByOrderId(int orderId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<SubElement>>($"api/subelements/filterbyorder/{orderId}");
                if (result == null)
                {
                    return new List<SubElement>();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<SubElement>();
            }
        }
        public async Task<List<SubElement>> GetSubElementsByWindowId(int windowId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<SubElement>>($"api/subelements/filterbywindow/{windowId}");
                if (result == null)
                {
                    return new List<SubElement>();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<SubElement>();
            }
        }
        public async Task<SubElement> GetSubElementById(int id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<SubElement>($"api/subelements/{id}");
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> CreateSubElement(SubElement subelEment)
        {
            try
            {
                var result = await _http.PostAsJsonAsync($"api/subelements/new", subelEment);
                return "Sub Element save successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> UpdateSubElement(SubElement subelEment)
        {
            try
            {
                var result = await _http.PutAsJsonAsync($"api/subelements", subelEment);
                return "Sub Element updated successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> DeleteSubElementById(int id)
        {
            try
            {
                var result = await _http.DeleteAsync($"api/subelements/{id}");
                return "Sub Element deleted successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
    }
}
