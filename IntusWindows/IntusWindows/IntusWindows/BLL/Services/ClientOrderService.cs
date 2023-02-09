using IntusWindows.Shared;
using IntusWindows.DAL.DataModels;
using System;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace IntusWindows.BLL.Services
{
    public class ClientOrderService : IClientOrderService
    {
        private readonly HttpClient _http;
        public ClientOrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Order>> GetOrders()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Order>>("api/orders");
                if (result == null)
                {
                    return new List<Order>();
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<Order>();
            }
        }
        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<Order>($"api/orders/{id}");
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<string> CreateOrder(Order order)
        {
            try
            {
                var result = await _http.PostAsJsonAsync($"api/orders/new", order);
                return "Order save successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> UpdateOrder(Order order)
        {
            try
            {
                var result = await _http.PutAsJsonAsync($"api/orders", order);
                return "Order updated successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
        public async Task<string> DeleteOrderById(int id)
        {
            try
            {
                var result = await _http.DeleteAsync($"api/orders/{id}");
                return "Order deleted successfully.";
            }
            catch (Exception ex)
            {
                return ex.GetErrorMessage();
            }
        }
    }
}
