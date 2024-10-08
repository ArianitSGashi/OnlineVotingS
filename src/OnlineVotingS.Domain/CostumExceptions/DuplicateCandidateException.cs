﻿namespace OnlineVotingS.Domain.CostumExceptions;

public class DuplicateCandidateException : Exception
{
    public DuplicateCandidateException(string message) : base(message)
    {
    }
    public DuplicateCandidateException(string fullName, int electionId, string party)
        : base($"A candidate already exists in election {electionId} for party '{party}'.")
    {
    }
}
