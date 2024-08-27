using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

    public class VotesPostDTO
    {
        public string UserID { get; set; } = null!;
        public int ElectionID { get; set; }
        public int CandidateID { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.UtcNow;
    }