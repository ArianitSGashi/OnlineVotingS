using OnlineVotingS.Domain.Enums;
namespace OnlineVotingS.API.Models.CandidateViewModels
{
    public class ViewCandidatesViewModel
    {
        public string CandidateID { get; set; }
        public string ElectionID { get; set; }
        public string FullName { get; set; }
        public string Party { get; set; }
        public string? Description { get; set; }
        public decimal Income { get; set; } // Format as currency in the view
        public string Works { get; set; }
    }
}
