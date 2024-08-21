using MediatR;
using OnlineVotingS.Domain.Entities;
using System.Collections.Generic;

namespace OnlineVotingS.Application.Services.Feedbacks.Requests.Queries;

public class GetAllFeedbacksQuery : IRequest<List<Feedback>>
{
}

