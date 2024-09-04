using OnlineVotingS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

public class DeleteVoterViewModel
{
    [Required(ErrorMessage = "ID is required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "ID must be exactly 10 digits long and contain only numbers")]
    public string Id { get; set; } = string.Empty;
}