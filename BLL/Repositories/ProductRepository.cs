using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using BLL.Interfaces;

namespace BLL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;
        public ProductRepository(Context _context)
        {
            context = _context;
        }


        public async Task<bool> Create(Product Prod)
        {
            try
            {
                context.Add(Prod);
                await context.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }


        public async Task<List<Product>> GetAll()
        {
            try
            {
                return await context.Products.ToListAsync();
            }
            catch { return new List<Product> { }; }
        }

        public Product GetByKey(int ID)
        {
            try
            {                
                return  context?.Products?.FirstOrDefault(i => i.Id == ID) ?? new Product();
            }
            catch { return new Product(); }
        }

        public  bool Delete(int ID)
        {
            try
            {                
                Product OldProd =  context?.Products?.FirstOrDefault(i => i.Id == ID) ?? new Product();
                context?.Remove(OldProd);
                 context?.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }


        public  bool Update(int ID, Product Prod)
        {
            try
            {                
                Product OldIns =  context?.Products?.FirstOrDefault(i => i.Id == ID) ?? new Product();

                OldIns = new Product()
                {
                    Id = ID,
                    Discout = Prod.Discout,
                    Price = Prod.Price,
                    SID = ID,
                    Description = Prod.Description,
                    Image = Prod.Image,
                    Name = Prod.Name
                };

                 context?.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public Product GetProductWithSection(string Name)
        {
            try
            {
                Product p =  context?.Products?.Include(p => p.Section).FirstOrDefault(p => p.Name == Name) ?? new Product();
                return p;
            }
            catch { return new Product(); }

        }

        public  bool Find(int id)
        {
            return context.Products.Any(p => p.Id == id);
        }

        public string GetSecNameOfProd(int id)
        {
            return context.Products?.Include(p => p.Section)?.FirstOrDefault(p => p.Id == id)?.Section?.Name ?? "";
        }
    }
}
