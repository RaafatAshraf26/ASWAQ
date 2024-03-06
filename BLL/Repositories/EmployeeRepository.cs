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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context context;
        public EmployeeRepository(Context _context)
        {
            context = _context;
        }

        public bool Create(Employee Emp)
        {
            try
            {
                context.Add(Emp);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByKeyAsync(string id)
        {
            try
            {
                return await context.Employees.Where(e => e.Id == id).Include(e => e.User).Include(e => e.EmployeeSections).ThenInclude(e=>e.Section).FirstOrDefaultAsync() ?? new Employee();
            }
            catch { return new Employee(); }
        }

        public async Task<List<Employee>> GetSvEmployeesByIdAsync(string id)
        {
            try
            {
                return await context.Employees.Where(e => e.SuperVisor_Id == id).Include(e=>e.EmployeeSections).ThenInclude(e=>e.Section).Include(e=>e.User).ToListAsync();
            }
            catch { return new List<Employee>(); }
        }

        public async Task<bool> Update(string id ,Employee Emp)
        {
            try
            {
                Employee employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id) ?? new();

                employee.Salary = Emp.Salary;
                employee.Address = Emp.Address;
                employee.SuperVisor_Id = Emp.SuperVisor_Id;
                employee.Age = Emp.Age;                
                context.SaveChanges();

                return true;
            }
            catch { return false; }
        }
    }
}
