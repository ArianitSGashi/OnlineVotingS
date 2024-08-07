using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

    public class CandidatesPostDTO
    {
        public int ElectionID { get; set; }
        public string FullName { get; set; } = null!;
        public string? Party { get; set; }
        public string? Description { get; set; }
        public decimal? Income { get; set; }
        public string? Works { get; set; }
    }