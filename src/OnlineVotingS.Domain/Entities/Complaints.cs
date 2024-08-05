using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Entities
{
    public class Complaints
    {
        /// <summary>
        /// Gets or sets the unique identifier for the complaint.
        /// </summary>
        [Key]
        public int ComplaintID { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the user who filed the complaint.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier of the election related to the complaint.
        /// </summary>
        [ForeignKey("Elections")]
        public int ElectionID { get; set; }
        /// <summary>
        /// Gets or sets the text of the complaint.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string ComplaintText { get; set; } = null!;
        /// <summary>
        /// Gets or sets the date the complaint was filed.
        /// </summary>
        public DateTime ComplaintDate { get; set; }
        /// <summary>
        /// Navigation property to the associated Election entity.
        /// </summary>
        public Elections Elections { get; set; } = null!;
    }
}
