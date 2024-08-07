using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class VotesPutDTO
    {
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public DateTime VoteDate { get; set; }
    }