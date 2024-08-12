using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.Voters
{
    public class ViewVoterViewModel
    {
        public List<VoterDto> Voters { get; set; } = new List<VoterDto>();
        public string SearchQuery { get; set; }
    }
    public class VoterDto
    {
        public string PhotoUrl { get; set; }
        public string VoterId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string State { get; set; }
        public string Parliamentary { get; set; }
        public string Assembly { get; set; }
    }
}
