using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.API.Models.VoterViewModels;

public class RepliedComplaintsViewModel
{
    public int RepliedComplaintID { get; set; }
    public string ElectionTitle { get; set; } = string.Empty;
    public int ComplaintID { get; set; }
    public string ComplaintText { get; set; } = string.Empty;
    public DateTime ComplaintDate { get; set; }
    public string ReplyText { get; set; } = string.Empty;
    public DateTime ReplyDate { get; set; }
    public IEnumerable<RepliedComplaints> RepliedComplaints { get; set; } = new List<RepliedComplaints>();
    public int CurrentPage { get; set; } = 1; // Current page of results
    public int TotalPages { get; set; } = 1; // Total number of pages available
    public bool HasPreviousPage => CurrentPage > 1; // Checks if there is a previous page
    public bool HasNextPage => CurrentPage < TotalPages; // Checks if there is a next page
}