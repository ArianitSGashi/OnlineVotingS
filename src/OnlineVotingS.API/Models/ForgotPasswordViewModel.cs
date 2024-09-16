using System.ComponentModel.DataAnnotations;

namespace OnlineVotingS.API.Models;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}