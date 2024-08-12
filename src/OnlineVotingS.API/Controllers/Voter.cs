using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.API.Models.Voters;

namespace OnlineVotingS.API.Controllers
{
    public class Voter : Controller
    {
        public IActionResult AddVoter()
        {
            var viewModel = new AddVoterViewModel();
            return View("Voter/AddVoter", viewModel);
        }

        [HttpPost]
        public IActionResult AddVoter(AddVoterViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("ViewVoter");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditDeleteVoter(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var viewModel = new EditDeleteVoterViewModel
            {
                VoterId = id
            };

            return View("Voter/EditDeleteVoter", viewModel);
        }

        [HttpPost]
        public IActionResult EditVoter(EditDeleteVoterViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("ViewVoter");
            }
            return View("EditDeleteVoter", model);
        }

        [HttpPost]
        public IActionResult DeleteVoter(EditDeleteVoterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ViewVoter");
            }
            return View("EditDeleteVoter", model);
        }

        [HttpGet]
        public IActionResult ViewVoter(string searchQuery)
        {

            var viewModel = new ViewVoterViewModel
            {
                SearchQuery = searchQuery,
            };

            return View("Voter/ViewVoter", viewModel);
        }
    }
}
