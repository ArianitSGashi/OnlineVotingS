﻿using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

public class GetRecentRepliedComplaintsQuery : IRequest<IEnumerable<RepliedComplaints>>
{
    public DateTime Date { get; set; }

    public GetRecentRepliedComplaintsQuery(DateTime date)
    {
        Date = date;
    }
}