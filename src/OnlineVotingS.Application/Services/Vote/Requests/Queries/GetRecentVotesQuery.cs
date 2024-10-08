﻿using MediatR;
using OnlineVotingS.Domain.Entities;
using FluentResults;

namespace OnlineVotingS.Application.Services.Vote.Requests.Queries;

public class GetRecentVotesQuery : IRequest<Result<IEnumerable<Votes>>>
{
      public DateTime Date { get; }

      public GetRecentVotesQuery(DateTime date)
      {
            Date = date;
      }
}