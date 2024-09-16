using AutoMapper;
using OnlineVotingS.Application.DTO;
using OnlineVotingS.Application.DTO.PostDTO;
using OnlineVotingS.Application.DTO.PutDTO;
using OnlineVotingS.Application.Services.Complaint.Requests.Commands;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Application.Mapping;

public class MappingProfile : Profile
    {
       public MappingProfile()
       {
          // CreateMap<Source, Destination>();
          CreateMap<Candidates, CandidatesPostDTO>().ReverseMap();
          CreateMap<Candidates, CandidatesPutDTO>().ReverseMap();
          CreateMap<Elections, ElectionsPostDTO>().ReverseMap();
          CreateMap<Elections, ElectionsPutDTO>().ReverseMap();
          CreateMap<Votes, VotesPostDTO>().ReverseMap();
          CreateMap<Votes, VotesPutDTO>().ReverseMap();
          CreateMap<Result, ResultPostDTO>().ReverseMap();
          CreateMap<Result, ResultPutDTO>().ReverseMap();
          CreateMap<Complaints, ComplaintsPostDTO>().ReverseMap();
          CreateMap<Complaints, ComplaintsPutDTO>().ReverseMap();
          CreateMap<Campaign, CampaignPostDTO>().ReverseMap();
          CreateMap<Campaign, CampaignPutDTO>().ReverseMap();
          CreateMap<Feedback, FeedbackPostDTO>().ReverseMap();
          CreateMap<Feedback, FeedbackPutDTO>().ReverseMap();
          CreateMap<RepliedComplaints, RepliedComplaintsPostDTO>().ReverseMap();
          CreateMap<RepliedComplaints, RepliedComplaintsPutDTO>().ReverseMap();

          CreateMap<RepliedComplaintsPostDTO, RepliedComplaints>();

    }
}
