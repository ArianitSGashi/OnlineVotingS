namespace OnlineVotingS.API.Models.VoterViewModels;

public class RepliedComplaintsViewModel
{
    public int RepliedComplaintID { get; set; }
    public int ComplaintID { get; set; }
    public string ComplaintText { get; set; } = string.Empty;
    public string ReplyText { get; set; } = string.Empty;
    public DateTime ReplyDate { get; set; }
}