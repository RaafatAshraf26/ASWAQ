using ASWAQ.Controllers;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace SuperMarket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderRepository orderRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IProductRepository productRepository;
        private readonly ICustomerRepository customerRepository;

        public CartController(UserManager<ApplicationUser> _userManager, IOrderRepository _orderRepository, ISectionRepository _sectionRepository, IProductRepository _productRepository, ICustomerRepository _customerRepository)
        {
            userManager = _userManager;
            orderRepository = _orderRepository;
            sectionRepository = _sectionRepository;
            productRepository = _productRepository;
            customerRepository = _customerRepository;
        }

        public async Task<IActionResult> Index()
        {

            string id = userManager.GetUserId(User);            

            List<Order> Cart = await orderRepository.GetAllByCustomerID(id);

            decimal TotalPrice = await customerRepository.GetTotalPriceAsync(id);

            ViewBag.TotalPrice = Math.Round(TotalPrice, 2).ToString();

            return View(Cart);
        }

        public async Task<IActionResult> ChangeCartAction(int ID, int change = 1, bool Add = false)
        {

            string CustomerID = userManager.GetUserId(User);            

            Order existsOrder = await orderRepository.GetByKeys(CustomerID, ID);

            if (existsOrder.ProductID != 0)
            {

                existsOrder.Total += existsOrder.Total + change >= 1 ? change : 0;
                orderRepository.Update(existsOrder.CustomerID, existsOrder.ProductID, existsOrder);

                if (!Add)
                {
                    decimal TotalPrice = await customerRepository.GetTotalPriceAsync(CustomerID);

                    TotalPrice = Math.Round(TotalPrice, 2);

                    return Json(new { Total = existsOrder.Total, TotalPrice = TotalPrice.ToString() });
                }

            }

            else
            {
                Order order = new Order() { CustomerID = CustomerID, ProductID = ID, Total = change, OrderDate = DateTime.Now };
                orderRepository?.Create(order);
                
            }

            string Name = productRepository.GetSecNameOfProd(ID);
            return RedirectToAction(nameof(SectionController.Index), nameof(SectionController).Replace("Controller", ""), new { Name });

        }
        
        public async Task<IActionResult> RemoveProduct(string CustomerID, int ProductID)
        {            
            Order order = await orderRepository.GetByKeys(CustomerID, ProductID);            

            orderRepository.Delete(order.CustomerID, order.ProductID);            

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckOut()
        {
            string CustomerID = userManager.GetUserId(User);
            
            decimal TotalPrice = await customerRepository.GetTotalPriceAsync(CustomerID);
            if (TotalPrice == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TotalPrice = Math.Round(TotalPrice, 2).ToString();
                                       
            Customer customer = await customerRepository.GetByKeyAsync(CustomerID);            

            return View(customer);                     
        }
        
        public async Task<IActionResult> FinalCheckOut()
        {            

            bool deleted = await orderRepository.DeleteAll(userManager.GetUserId(User));
            if (deleted)
            {
                return RedirectToAction("Index", "Home");
            }

            else return RedirectToAction(nameof(CheckOut));
        }
    }
}