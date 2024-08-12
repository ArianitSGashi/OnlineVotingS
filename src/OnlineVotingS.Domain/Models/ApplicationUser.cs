using Microsoft.AspNetCore.Identity;
using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Models;

    public class ApplicationUser :IdentityUser
    {
        public string VoterId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }