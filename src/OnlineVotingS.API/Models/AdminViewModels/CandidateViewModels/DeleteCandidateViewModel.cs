using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels
{
    public class DeleteCandidateViewModel
    {
        [Required(ErrorMessage = "Please select a candidate to delete")]
        public int CandidateID { get; set; }
        public List<SelectListItem> AvailableCandidates { get; set; } = new List<SelectListItem>();
    }
}