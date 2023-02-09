using IntusWindows.DAL.DataModels;

namespace IntusWindows.BLL.Services
{
    public interface IClientSubElementService
    {
        Task<List<SubElement>> GetSubElements();
        Task<List<SubElement>> GetSubElementsByOrderId(int orderId);
        Task<List<SubElement>> GetSubElementsByWindowId(int windowId);
        Task<SubElement> GetSubElementById(int id);
        Task<string> DeleteSubElementById(int id);
        Task<string> CreateSubElement(SubElement order);
        Task<string> UpdateSubElement(SubElement order);
    }
}
