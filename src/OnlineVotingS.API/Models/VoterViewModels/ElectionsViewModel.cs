using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.API.Models.VoterViewModels;

public class ElectionsViewModel
{
    public int ElectionID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeSpan EndTime { get; set; }
    public ElectionStatus Status { get; set; }
    public bool UserHasVoted { get; set; }
    public IEnumerable<Elections> Elections { get; set; } = new List<Elections>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
