using ASWAQ.ViewModels;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASWAQ.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IWorkInRepository workInRepository;
        private readonly IProductRepository productRepository;

        public AdminController(UserManager<ApplicationUser> _userManager, IEmployeeRepository _employeeRepository, ISectionRepository _sectionRepository, IWorkInRepository _workInRepository,IProductRepository _productRepository )
        {
            userManager = _userManager;
            employeeRepository = _employeeRepository;
            sectionRepository = _sectionRepository;
            workInRepository = _workInRepository;
            productRepository = _productRepository;
        }


        public IActionResult Index()
        {
            // var roles = await roleManager.Roles.ToListAsync();

            return View();
        }

        public async Task<IActionResult> Employees()
        {
            string superVisor = userManager.GetUserId(User);

            return View(await employeeRepository.GetSvEmployeesByIdAsync(superVisor));
        }

        public async Task<IActionResult> AddEmployee()
        {
            ViewBag.Sections = await sectionRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel newEmp)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new()
                {
                    UserName = newEmp.UserName,
                };

                IdentityResult result = await userManager.CreateAsync(newUser, newEmp.Password);
                if (result.Succeeded)
                {
                    string UserID = userManager.GetUserId(User);

                    Employee employee = new() { Id = newUser.Id, Salary = newEmp.Salary, SuperVisor_Id = UserID };
                    employeeRepository.Create(employee);

                    foreach (var SectionID in newEmp.SectionsId)
                    {
                        await workInRepository.CreateAsync(new WorkIn() { EID = employee.Id, SID = SectionID });
                    }

                    return RedirectToAction(nameof(Employees));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(newEmp);
        }


        public async Task<IActionResult> EditEmployee(string EmpID)
        {
            Employee employee = await employeeRepository.GetByKeyAsync(EmpID);

            ViewBag.Sections = await sectionRepository.GetAll();

            EditEmployeeViewModel editEmployee = new()
            {

                Id = employee.Id,
                UserName = employee.User.UserName,
                Salary = employee.Salary
            };

            foreach (var emp in employee.EmployeeSections)
            {
                editEmployee?.SectionsId?.Add(emp.SID);
            }


            return View(editEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel editEmployee)
        {

            if (ModelState.IsValid)
            {
                await employeeRepository.Update(editEmployee.Id, new Employee()
                {
                    Salary = editEmployee.Salary,
                    SuperVisor_Id = userManager.GetUserId(User)
                });

                await workInRepository.DeleteAllAsync(editEmployee.Id);

                foreach (var SectionID in editEmployee.SectionsId)
                {
                    await workInRepository.CreateAsync(new WorkIn() { EID = editEmployee.Id, SID = SectionID });
                }

                ApplicationUser user = await userManager.FindByIdAsync(editEmployee.Id);
                user.UserName = editEmployee.UserName;
                IdentityResult result = await userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Employees));
            }

            return RedirectToAction(nameof(EditEmployee), new { EmpId = editEmployee.Id });
        }





        public async Task<IActionResult> Sections()
        {
            return View(await sectionRepository.GetAll());
        }

        public async Task<IActionResult> Section(string secName)
        {
            return View(await sectionRepository.GetSectionWithProductsAsync(secName));
        }        

        public IActionResult AddSection()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(AddSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Section section = new() { Name = model.Name };

                using (var memorystream = new MemoryStream())
                {
                    await model.Image.CopyToAsync(memorystream);
                    section.Image = memorystream.ToArray();

                    sectionRepository.Create(section);
                }

                return RedirectToAction(nameof(AddSection));

            }

            return View(model);
        }

        public async Task<IActionResult> RemoveSection(int sectionID)
        {
            await sectionRepository.Delete(sectionID);

            return RedirectToAction(nameof(Sections));
        }





        public IActionResult AddProduct(int SectionID)
        {
            ViewBag.SectionID = SectionID;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProduct)
        {
            if (ModelState.IsValid)
            {
                Product product = new() { Name = addProduct.Name, Description = addProduct.Description, Price = addProduct.Price, SID = addProduct.SectionID };

                using (MemoryStream memoryStream = new())
                {
                    await addProduct.Image.CopyToAsync(memoryStream);

                    product.Image = memoryStream.ToArray();

                    await productRepository.Create(product);

                    return RedirectToAction(nameof(AddProduct), new {SectionID = addProduct.SectionID});
                }
            }

            return View(addProduct);
        }        

    }
}
