using System.ComponentModel.DataAnnotations;
using DataType = Swashbuckle.AspNetCore.SwaggerGen.DataType;

namespace OnlineVotingS.API.Models;

public class ResetPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, and one number.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Please confirm your password")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }
}