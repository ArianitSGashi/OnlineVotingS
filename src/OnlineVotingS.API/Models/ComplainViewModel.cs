﻿namespace OnlineVotingS.API.Models;

    public class ComplainViewModel
    {
     public int ComplaintID { get; set; }
     public string UserID { get; set; } = string.Empty;
     public int ElectionID { get; set; }
     public string ComplaintText { get; set; } = string.Empty;
     public DateTime ComplaintDate { get; set; }
    }