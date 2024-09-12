using MediatR;

namespace OnlineVotingS.Application.Services.Election.Requests.Commands;

public class CompleteElectionCommand : IRequest<bool>
{
    public string Title { get; set; }

    public CompleteElectionCommand(string title)
    {
        Title = title;
    }
}