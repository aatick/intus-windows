using IntusWindows.DAL.DataModels;

namespace IntusWindows.BLL.Services
{
    public interface IClientWindowService
    {
        Task<List<Window>> GetWindows();
        Task<List<Window>> GetWindowsByOrderId(int orderId);
        Task<Window> GetWindowById(int id);
        Task<string> DeleteWindowById(int id);
        Task<string> CreateWindow(Window window);
        Task<string> UpdateWindow(Window window);
    }
}
