using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PostDTO;

    public class ComplaintsPostDTO
    {
        public string UserID { get; set; }
        public int ElectionID { get; set; }
        public string ComplaintText { get; set; } = null!;
        public DateTime ComplaintDate { get; set; }
    }