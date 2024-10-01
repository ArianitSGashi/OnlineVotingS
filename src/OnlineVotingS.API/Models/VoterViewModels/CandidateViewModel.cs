using OnlineVotingS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.VoterViewModels;

public class CandidateViewModel
{
    public int CandidateID { get; set; }
    public int ElectionID { get; set; }

    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    public string? Party { get; set; } = string.Empty;
    public string? Description { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Income { get; set; }

    public string? Works { get; set; } = string.Empty;

    public int CurrentPage { get; set; } = 1;
    public int TotalCount { get; set; } = 0; // Total candidates count
    public int PageSize { get; set; } = 10; // Number of items per page

    public IEnumerable<Candidates> Candidates { get; set; } = new List<Candidates>();

}

