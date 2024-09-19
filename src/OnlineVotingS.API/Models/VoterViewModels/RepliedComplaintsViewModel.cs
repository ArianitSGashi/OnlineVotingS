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
}