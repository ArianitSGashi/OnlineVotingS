using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

public class DeleteVoterViewModel
{
    [Required]
    public string Id { get; set; } = string.Empty;
}