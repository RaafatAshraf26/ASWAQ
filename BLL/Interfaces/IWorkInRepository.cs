using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWorkInRepository
    {
        Task<bool> CreateAsync(WorkIn workIn);

        Task<bool> DeleteAllAsync(string EmpId);
    }
}
