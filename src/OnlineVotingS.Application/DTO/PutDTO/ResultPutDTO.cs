using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

public class ResultPutDTO
{
    public int ResultID { get; set; }
    public int ElectionID { get; set; }
    public int CandidateID { get; set; }
    public int TotalVotes { get; set; }

}