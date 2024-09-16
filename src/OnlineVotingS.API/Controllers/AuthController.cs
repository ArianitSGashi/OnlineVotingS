using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity?.IsAuthenticated == true)
            {
                _signInManager.SignOutAsync().Wait();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else if (roles.Contains("Voter"))
                        {
                            return RedirectToAction("VoterDashboard", "Voter");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    FathersName = model.FathersName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    PhoneNumber = model.MobileNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Voter");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Auth");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("GuestDashboard", "Guest");
        }
    }
}
