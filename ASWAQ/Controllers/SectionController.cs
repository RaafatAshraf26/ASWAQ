using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ASWAQ.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionRepository sectionsRepository;
        private readonly IProductRepository productRepository;
        public SectionController(ISectionRepository _sectionsRepository, IProductRepository _productRepository)
        {
            sectionsRepository = _sectionsRepository;
            productRepository = _productRepository;

        }

        public async Task<IActionResult> Index(string Name)
        {
            Section section = await sectionsRepository.GetSectionWithProductsAsync(Name);

            return View(section);
        }

        public IActionResult Product(string Name)
        {
            Product product = productRepository.GetProductWithSection(Name);

            return View(product);
        }
    }
}
