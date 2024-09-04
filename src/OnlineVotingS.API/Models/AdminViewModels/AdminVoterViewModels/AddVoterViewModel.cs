using OnlineVotingS.API.Models.DataAttributes;
using OnlineVotingS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.AdminViewModels.AdminVoterViewModels;

public class AddVoterViewModel
{
    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "ID must be exactly 10 digits long and contain only numbers")]
    public string Id { get; set; } = string.Empty;
    [MinLength(2, ErrorMessage = "Username must be at least 2 characters long")]
    public string UserName { get; set; } = string.Empty;
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    public string Name { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    [MinimumAge(18, ErrorMessage = "You must be at least 18 years old.")]
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    [RegularExpression(@"^[\d\+\-\s]+$", ErrorMessage = "Mobile number can only contain digits, spaces, and the characters + and -")]
    public string MobileNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, and one number.")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}