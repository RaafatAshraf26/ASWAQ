using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ICustomerRepository 
    {
        bool Create(Customer Cus);

        List<Customer> GetAll();

        Task<Customer> GetByKeyAsync(string ID);

        bool Delete(string ID);

        bool Update(string ID, Customer Cus);

        public bool Find(string ID);

        Task<decimal> GetTotalPriceAsync(string ID);

        Task<int> GetOrdersCount(string ID);
    }
}
