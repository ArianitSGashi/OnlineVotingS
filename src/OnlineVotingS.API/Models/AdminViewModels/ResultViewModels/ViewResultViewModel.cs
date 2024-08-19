using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineVotingS.API.Models.AdminViewModels.ResultViewModels;

public class ViewResultViewModel
{
    public int ResultID { get; set; }
    public int ElectionID { get; set; }
    public string ElectionTitle { get; set; } = string.Empty;
    public int CandidateID { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public int TotalVotes { get; set; }
}
