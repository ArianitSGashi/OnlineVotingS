using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminVoterViewModels;

public class DeleteVoterViewModel
{
    public string VoterId { get; set; } = string.Empty;
}