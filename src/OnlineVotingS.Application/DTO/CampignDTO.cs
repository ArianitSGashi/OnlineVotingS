using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO
{
    public class CampaignDTO
    {
        public int CampaignID { get; set; }
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }

        // TODO: Add properties for the date and time of the last update, the associated election, and the associated candidate.

    }
}
