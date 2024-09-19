﻿using OnlineVotingS.Domain.Enums;

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
}
