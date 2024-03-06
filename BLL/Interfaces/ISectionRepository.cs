using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ISectionRepository
    {
        bool Create(Section Sec);

        Task<List<Section>> GetAll();

        Section GetByKey(int id);

        Task<bool> Delete(int id);

        bool Update(int id, Section Sec);        

        Task<Section> GetSectionWithProductsAsync(string Name);
    }
}
