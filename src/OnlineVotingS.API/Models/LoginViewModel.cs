using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models;

public class LoginViewModel
{
    [MinLength(2, ErrorMessage = "Username must be at least 2 characters long")]
    public string UserName { get; set; } = string.Empty;
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, and one number.")]
    public string Password { get; set; } = string.Empty;
}