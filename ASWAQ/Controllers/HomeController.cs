using ASWAQ.Models;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASWAQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISectionRepository sectionRepository;        
        public HomeController(ISectionRepository _sectionRepository)
        {
            sectionRepository = _sectionRepository;         
        }

        public async Task<IActionResult> Index()
        {            
            var sections = await sectionRepository.GetAll();
            return View(sections);
        }
    }
}
