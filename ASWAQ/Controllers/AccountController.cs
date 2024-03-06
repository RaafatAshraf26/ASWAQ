using DAL.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BLL.Interfaces;
using ASWAQ.ViewModels;
using System.Text;


namespace ASWAQ.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICustomerRepository customerRepository;        

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ICustomerRepository _customerRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            customerRepository = _customerRepository;            
        }

        public IActionResult Register()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel NewUser)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = NewUser.UserName,
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    Email = NewUser.Email,                    
                    PhoneNumber = NewUser.PhoneNumber,                     
                };

                IdentityResult result = await userManager.CreateAsync(user, NewUser.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                    await signInManager.SignInAsync(user, NewUser.RememberMe);

                    customerRepository.Create(new()
                    {
                        Id = user.Id
                    });

                    return RedirectToAction("Index", "Home");
                }                                
                else
                {
                    foreach (var error in result.Errors)
                    {                        
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(NewUser);
        }

        public async Task<IActionResult> UserNameValid(string UserName , string id)
        {
            if (id != null) return Json(true);

            ApplicationUser user = await userManager.FindByNameAsync(UserName);

            if (User.Identity.IsAuthenticated)
            {
                return Json(User.Identity.Name == UserName || user == null);
            }

            return Json(user == null);
        }        


        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userLogin.UserName);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);
                    
                    if (result.Succeeded)
                    {                                                
                        await signInManager.SignInAsync(user, userLogin.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }                    
                }
            }

            ModelState.AddModelError("", "Check Your Email or Password");
            return View(userLogin);
        }



        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();            

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);

            ProfileViewModel profile = new()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,                
            };            

            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel UserChage)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(User?.Identity?.Name);
                if (user != null)
                {
                    string UserName = user.UserName;

                    user.UserName = UserChage.UserName;
                    user.FirstName = UserChage.FirstName;
                    user.LastName = UserChage.LastName;
                    user.Email = UserChage.Email;
                    user.PhoneNumber = UserChage.PhoneNumber;                
                                        
                    IdentityResult res = await userManager.UpdateAsync(user);
                    if (res.Succeeded)
                    {                                                
                        return View(UserChage);
                    }
                }
            }

            return View(UserChage);
        }

        [Authorize]
        public async Task<IActionResult> EditNumber(string PhoneNumber)
        {

            ApplicationUser user = await userManager.GetUserAsync(User);

            if (user != null)
            {                
                user.PhoneNumber = PhoneNumber;
                await userManager.UpdateAsync(user);                
            }

            return RedirectToAction("CheckOut", "Cart");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.GetUserAsync(User);

                string token = await userManager.GeneratePasswordResetTokenAsync(user);

                IdentityResult result = await userManager.ResetPasswordAsync(user, token, resetPassword.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }                        

            return View(resetPassword);
        }

    }
    
}
