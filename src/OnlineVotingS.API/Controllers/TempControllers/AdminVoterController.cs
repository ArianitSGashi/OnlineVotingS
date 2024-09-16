using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminVoterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminVoterController> _logger;

        public AdminVoterController(UserManager<ApplicationUser> userManager, ILogger<AdminVoterController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        // GET: /AdminVoter/AddVoter
        public IActionResult AddVoter()
        {
            return View("~/Views/Admin/AdminVoter/AddVoter.cshtml");
        }

        // POST: /AdminVoter/AddVoter
        [HttpPost]
        public async Task<IActionResult> AddVoter(AddVoterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"Validation error: {error.ErrorMessage}");
                    }
                }
                return View("~/Views/Admin/AdminVoter/AddVoter.cshtml", model);
            }

            try
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
                    return RedirectToAction(nameof(ViewVoters));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogError($"User creation error: {error.Description}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddVoter: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }

            return View("~/Views/Admin/AdminVoter/AddVoter.cshtml", model);
        }

        public IActionResult EditVoter()
        {
            return View("~/Views/Admin/AdminVoter/EditVoter.cshtml", new EditVoterViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> GetVoterDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { error = "Voter ID cannot be empty" });
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { error = "Voter not found" });
            }

            var voterDetails = new
            {
                userName = user.UserName,
                email = user.Email,
                name = user.Name,
                fathersName = user.FathersName,
                gender = user.Gender.ToString(),
                dateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd"),
                address = user.Address,
                mobileNumber = user.PhoneNumber
            };

            return Json(voterDetails);
        }

        [HttpPost]
        public async Task<IActionResult> EditVoter(EditVoterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Name = model.Name;
                user.FathersName = model.FathersName;
                user.Gender = model.Gender;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                user.PhoneNumber = model.MobileNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ViewVoters));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("~/Views/Admin/AdminVoter/EditVoter.cshtml", model);
        }

        [HttpGet]
        public IActionResult DeleteVoter()
        {
            return View("~/Views/Admin/AdminVoter/DeleteVoter.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVoter(DeleteVoterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/AdminVoter/DeleteVoter.cshtml", model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Voter not found.");
                return View("~/Views/Admin/AdminVoter/DeleteVoter.cshtml", model);
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ViewVoters));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("~/Views/Admin/AdminVoter/DeleteVoter.cshtml", model);
        }

        // GET: /AdminVoter/ViewVoters
        public async Task<IActionResult> ViewVoters()
        {
            var voters = await _userManager.GetUsersInRoleAsync("Voter");
            var model = voters.Select(v => new ViewVoterViewModel
            {
                Id = v.Id,
                UserName = v.UserName ?? string.Empty,
                Email = v.Email ?? string.Empty,
                Name = v.Name,
                FathersName = v.FathersName,
                Gender = v.Gender,
                DateOfBirth = v.DateOfBirth,
                Address = v.Address,
                MobileNumber = v.PhoneNumber ?? string.Empty
            }).ToList();

            return View("~/Views/Admin/AdminVoter/ViewVoters.cshtml", model);
        }
    }
}