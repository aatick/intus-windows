using IntusWindows.DAL.DataModels;

namespace IntusWindows.BLL.Services
{
    public interface IClientOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int id);
        Task<string> DeleteOrderById(int id);
        Task<string> CreateOrder(Order order);
        Task<string> UpdateOrder(Order order);
    }
}
