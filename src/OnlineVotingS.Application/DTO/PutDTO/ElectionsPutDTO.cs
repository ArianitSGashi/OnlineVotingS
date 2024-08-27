using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.DTO.PutDTO;

    public class ElectionsPutDTO
    {
        public int ElectionID { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    // TODO: Add properties for the date and time of the created at and last update.

}