using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> Create(Product Prod);

        Task<List<Product>> GetAll();

        Product GetByKey(int ID);

        bool Delete(int ID);

        bool Update(int ID, Product Prod);

        Product GetProductWithSection(string Name);

        string GetSecNameOfProd(int id);


        bool Find(int id);
    }
}