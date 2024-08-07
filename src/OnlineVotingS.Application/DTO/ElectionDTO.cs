using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO;
  
        public class ElectionDTO
        {
            public int ElectionID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            // TODO: Add properties for:The date and time of the election's creation,the date and time of the last update to the election.

        }
   
