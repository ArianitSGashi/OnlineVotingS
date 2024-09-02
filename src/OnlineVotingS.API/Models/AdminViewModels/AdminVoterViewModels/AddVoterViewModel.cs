using OnlineVotingS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

public class AddVoterViewModel
{
    [Required]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}