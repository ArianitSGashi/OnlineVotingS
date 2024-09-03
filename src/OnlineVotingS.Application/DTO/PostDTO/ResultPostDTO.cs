using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

public class ResultPostDTO
{
    public int ElectionID { get; set; }
    public int CandidateID { get; set; }
    public int TotalVotes { get; set; }
}