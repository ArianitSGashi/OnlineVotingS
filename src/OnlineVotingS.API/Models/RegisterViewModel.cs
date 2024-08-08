namespace OnlineVotingS.API.Models;

public class RegisterViewModel
{
    public string VoterId { get; set; }
    public string Name { get; set; }
    public string FathersName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string MobileNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassowrd { get; set; }
}

public enum Gender
{
    Male,
    Female
}

