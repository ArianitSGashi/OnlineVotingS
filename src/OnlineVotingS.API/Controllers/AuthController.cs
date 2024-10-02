using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models;
using OnlineVotingS.API.Models.VoterViewModels;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("GuestDashboard", "Guest");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return View();
            }

            return Login();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                ViewBag.Message = "Your password has been changed.";
                return View();
            }

            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            var callbackUrl = Url.Action("ResetPassword", "Auth", 
                new { token, email = model.Email }, 
                protocol: Request.Scheme);
            
            await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Reset your password by clicking <a href='{callbackUrl}'>here</a>.");
            
            return RedirectToAction("ForgotPasswordConfirmation");
        }
        
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult ResetPassword(string token = null, string email = null)
        {
            if (token == null || email == null)
            {
                return BadRequest("Invalid password reset token.");
            }

            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
