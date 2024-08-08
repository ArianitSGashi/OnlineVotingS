namespace OnlineVotingS.API.Models
{
    public class RegisterViewModel
    {
        public string VoterId{ get; set; }
        public string Name { get; set; }
        public string FathersName{ get; set; }
        public string Gender{ get; set; }
        public DateTime DateOfBirth{ get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassowrd{ get; set; }
    }
}
