using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO
{
    public class ResultDTO
    {
        public int ResultID { get; set; }
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public int TotalVotes { get; set; }
        public string ElectionName { get; set; }
        public string CandidateName { get; set; }
    }
}
