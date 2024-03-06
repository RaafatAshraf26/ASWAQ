using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

using BLL.Interfaces;

namespace BLL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        Context context;
        public OrderRepository(Context _context)
        {
            context = _context;
        }

        public bool Create(Order Ord)
        {
            try
            {
                 context.Add(Ord);
                 context.SaveChanges();
                return true;
            }
            catch { return false; }

        }

        public async Task<Order> GetByKeys(string CustomerID, int ID)
        {
            try
            {
                return  await context.Orders.FirstOrDefaultAsync(i => i.CustomerID == CustomerID && i.ProductID == ID) ?? new Order();
            }
            catch { return new Order(); }
        }

        public  List<Order> GetAll()
        {
            try
            {
                return  context.Orders.ToList();
            }
            catch { return new List<Order> { }; }
        }

        public  bool Delete(string UN, int ID)
        {
            try
            {
                Order OldOrd =  context.Orders.FirstOrDefault(i => i.CustomerID == UN && i.ProductID == ID) ?? new Order();
                context.Remove(OldOrd);
                 context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public  bool Update(string UN, int ID, Order Ord)
        {
            try
            {
                Order OldOrd =  context.Orders.FirstOrDefault(i => i.CustomerID == UN && i.ProductID == ID) ?? new Order();

                OldOrd = new Order()
                {
                    CustomerID = Ord.CustomerID,
                    ProductID = Ord.ProductID,
                    OrderDate = Ord.OrderDate,
                    Total = Ord.Total,
                };

                 context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }
                
        public  async Task<List<Order>> GetAllByCustomerID(string id)
        {
            try
            {
                return await context.Orders.Where(o => o.CustomerID == id).Include(o => o.Product).ThenInclude(p => p.Section).ToListAsync();
            }
            catch { return new List<Order>() { }; }
        }

        public async Task<bool> DeleteAll(string? CustomerID)
        {
            try
            {
                List<Order> orders = await context.Orders.Where(o => o.CustomerID == CustomerID).ToListAsync();

                foreach (Order o in orders)
                    context.Orders.Remove(o);

                context.SaveChanges();
                return true;
            }
            catch { return false; }

        }
    }
}
