using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class CampaignPutDTO
    {
        public int CampaignID { get; set; }
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // TODO: Add properties for the date and time of the created at and last update.

    }