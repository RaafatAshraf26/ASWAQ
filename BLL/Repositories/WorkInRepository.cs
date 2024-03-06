using BLL.Interfaces;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class WorkInRepository : IWorkInRepository
    {
        private readonly Context context;
        public WorkInRepository(Context _context) 
        {
            context = _context;
        }

        public async Task<bool> CreateAsync(WorkIn work)
        {
            try
            {
                context.Add(work);
                await context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DeleteAllAsync(string EmpID)
        {
            try
            {
                var WorksEmp = await context.WorkIn.Where(w => w.EID == EmpID).ToListAsync();

                foreach (var Work in WorksEmp) context.Remove(Work);

                context.SaveChanges();

                return true;
            }

            catch { return false; }

        }
    }
}
