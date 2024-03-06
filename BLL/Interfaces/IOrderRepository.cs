

using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IOrderRepository
    {

        bool Create(Order Cus);

        List<Order> GetAll();

        Task<Order> GetByKeys(string CustomerID, int ID);

        bool Delete(string CustomerID, int ID);

        Task<bool> DeleteAll(string? CustomerID);

        bool Update(string CustomerID, int ID, Order Cus);
        
        Task<List<Order>> GetAllByCustomerID(string CustomerID);
    }
}
