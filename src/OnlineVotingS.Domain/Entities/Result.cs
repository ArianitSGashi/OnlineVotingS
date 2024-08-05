using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Entities
{
    public class Result
    {
        /// <summary>
        /// Unique identifier for the result.
        /// </summary>
        [Key]
        public int ResultID { get; set; }
        /// <summary>
        /// Unique identifier for the election associated with the result.
        /// </summary>
        [ForeignKey("Elections")]
        public int ElectionID { get; set; }
        /// <summary>
        /// Unique identifier for the candidate associated with the result.
        /// </summary>
        [ForeignKey("Candidates")]
        public int CandidateID { get; set; }
        /// <summary>
        /// Total number of votes received by the candidate.
        /// </summary>
        public int TotalVotes { get; set; }
        // Navigation Properties
        /// <summary>
        /// Election object associated with the result, representing the election details.
        /// </summary>
        public Elections Elections { get; set; } = null!;
        /// <summary>
        /// Candidate object associated with the result, representing the candidate details.
        /// </summary>
        public Candidates Candidates { get; set; } = null!;
    }
}
