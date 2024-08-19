using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.API.Models.AdminViewModels.CandidateViewModels;

public class ViewCandidatesViewModel
{
    public string CandidateID { get; set; } = string.Empty;
    public string ElectionID { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Party { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Income { get; set; }
    public string Works { get; set; } = string.Empty;
}
