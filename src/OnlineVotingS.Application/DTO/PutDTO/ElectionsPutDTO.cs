using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

public class ElectionsPutDTO
{
    public int ElectionID { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateOnly EndDate { get; set; }
    public TimeSpan EndTime { get; set; }
    public ElectionStatus Status { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    // TODO: Add properties for the date and time of the created at and last update.

}