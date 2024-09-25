using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRepliedComplaintByIdQuery : IRequest<Result<RepliedComplaints>>
{
    public int RepliedComplaintId { get;}

    public GetRepliedComplaintByIdQuery(int repliedComplaintId)
    {
        RepliedComplaintId = repliedComplaintId;
    }
}