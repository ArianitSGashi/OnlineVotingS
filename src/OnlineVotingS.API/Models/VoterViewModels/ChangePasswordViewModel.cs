using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models.VoterViewModels;

public class ChangePasswordViewModel
{
    public string OldPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, and one number.")]
    public string NewPassword { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
