using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        bool Create(Employee Emp);

        List<Employee> GetAll();

        Task<Employee> GetByKeyAsync(string id);

        Task<List<Employee>> GetSvEmployeesByIdAsync(string id);

        bool Delete(string id);

        Task<bool> Update(string id,Employee Emp);


    }
}
