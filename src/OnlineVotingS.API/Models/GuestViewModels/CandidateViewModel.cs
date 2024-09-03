namespace OnlineVotingS.API.Models.GuestViewModels;

public class CandidateViewModel
{
    public int CandidateID { get; set; }
    public int ElectionID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Party { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Income { get; set; }
    public string Works { get; set; } = string.Empty;
}