namespace OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

public interface IElectionViewModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? StartDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeSpan? EndTime { get; set; }
}