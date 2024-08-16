using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class RepliedComplaintsPutDTO
    {
        public int RepliedComplaintID { get; set; }
        public int ComplaintID { get; set; }
        public string ReplyText { get; set; } = null!;
        public DateTime ReplyDate { get; set; }
    }