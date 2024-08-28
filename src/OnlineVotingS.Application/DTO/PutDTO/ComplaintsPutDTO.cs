using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class ComplaintsPutDTO
    {
        public int ComplaintID { get; set; }
        public string UserID { get; set; } = null!;
        public int ElectionID { get; set; }
        public string ComplaintText { get; set; } = null!;
        public DateTime ComplaintDate { get; set; }
    }