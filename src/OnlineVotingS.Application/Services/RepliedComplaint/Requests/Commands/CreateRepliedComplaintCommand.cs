using MediatR;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Application.Services.RepliedComplaint.Requests.Commands;

    public class CreateRepliedComplaintCommand : IRequest<RepliedComplaints>
    {
        public RepliedComplaintsPostDTO RepliedComplaint { get; }

        public CreateRepliedComplaintCommand(RepliedComplaintsPostDTO repliedComplaint)
        {
            RepliedComplaint = repliedComplaint;
        }
    }