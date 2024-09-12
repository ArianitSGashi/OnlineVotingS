namespace OnlineVotingS.Domain.CostumExceptions;

public class InvalidVoteException : Exception
{
    public InvalidVoteException(string message) : base(message)
    {
    }
}