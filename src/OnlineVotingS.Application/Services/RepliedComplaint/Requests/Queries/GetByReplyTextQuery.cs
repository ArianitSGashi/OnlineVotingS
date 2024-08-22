using MediatR;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Queries;

    public class GetByReplyTextQuery : IRequest<IEnumerable<RepliedComplaints>>
    {
        public string ReplyText { get; set; }

        public GetByReplyTextQuery(string replyText)
        {
            ReplyText = replyText;
        }
    }