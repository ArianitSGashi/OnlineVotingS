using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

    public class RepliedComplaintsPostDTO
    {
        public int ComplaintID { get; set; }
        public string ReplyText { get; set; } = null!;
        public DateTime ReplyDate { get; set; } = DateTime.UtcNow;
    }