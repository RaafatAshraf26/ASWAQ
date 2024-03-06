using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;



namespace BLL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        Context context;
        public CustomerRepository(Context _context)
        {
            context = _context;
        }

        public bool Create(Customer Cus)
        {
            try
            {
                context.Add(Cus);
                context.SaveChanges();
                return true;
            }
            catch { return false; }

        }

        public List<Customer> GetAll()
        {
            try
            {
                return context.Customers.ToList();
            }
            catch { return new List<Customer> { }; }
        }

        public async Task<Customer> GetByKeyAsync(string id)
        {
            try
            {
                return await context.Customers.Include(c=>c.User).FirstOrDefaultAsync(o => o.Id == id) ?? new Customer();
            }
            catch { return new Customer(); }
        }

        public bool Delete(string id)
        {
            try
            {
                Customer OldDept = context.Customers.FirstOrDefault(o => o.Id == id) ?? new Customer();
                context.Remove(OldDept);
                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Update(string id, Customer Cus)
        {
            try
            {
                Customer OldIns = context.Customers.FirstOrDefault(o => o.Id == id) ?? new Customer();

                OldIns = new Customer()
                {
                    Id = Cus.Id,                    
                };

                context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Find(string ID)
        {
            return context.Customers.Any(o => o.Id == ID);
        }

        public async Task<decimal> GetTotalPriceAsync(string ID)
        {
            return await context.Orders.Where(o => o.CustomerID == ID).Include(o => o.Product).
                ThenInclude(p => p.Section).Select(c => c.Product.Price * c.Total).SumAsync();
        }

        public async Task<int> GetOrdersCount(string ID)
        {
            return await context.Orders.CountAsync(o => o.CustomerID == ID);
        }


    }
}
