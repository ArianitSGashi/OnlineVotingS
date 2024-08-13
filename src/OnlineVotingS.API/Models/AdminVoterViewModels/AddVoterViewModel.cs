using OnlineVotingS.Domain.Enums;

namespace OnlineVotingS.API.Models.AdminVoterViewModels;

public class AddVoterViewModel
{
    public string VoterId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}