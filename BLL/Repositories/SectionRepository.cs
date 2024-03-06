using DAL.Context;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using BLL.Interfaces;

namespace BLL.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly Context context;
        public SectionRepository(Context _context)
        {
            context = _context;
        }


        public bool Create(Section Sec)
        {
            try
            {
                context?.Add(Sec);
                context?.SaveChanges();

                return true;
            }
            catch { return false; }

        }

        public async Task<List<Section>> GetAll()
        {
            try
            {
                return await context.Sections.ToListAsync() ?? new List<Section>();
            }
            catch { return new List<Section> { }; }
        }

        public Section GetByKey(int id)
        {
            try
            {
                return context?.Sections?.FirstOrDefault(i => i.Id == id) ?? new Section();
            }
            catch { return new Section(); }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Section OldSec = await context.Sections.FirstOrDefaultAsync(i => i.Id == id) ?? new Section();
                context?.Remove(OldSec);
                context?.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Update(int id, Section Sec)
        {
            try
            {
                Section OldIns = context?.Sections?.FirstOrDefault(i => i.Id == id) ?? new Section();

                OldIns = new Section()
                {
                    Id = id,                    
                    Image = Sec.Image,
                    Name = Sec.Name
                };

                context?.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<Section> GetSectionWithProductsAsync(string Name)
        {
            try
            {
                return await context.Sections.Where(s => s.Name == Name).Include(s => s.Products).FirstOrDefaultAsync() ?? new Section();
            }
            catch { return new Section(); }
        }
    }
}
