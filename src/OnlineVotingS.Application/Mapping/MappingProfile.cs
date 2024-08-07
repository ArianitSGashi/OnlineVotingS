using AutoMapper;
using OnlineVotingS.Application.DTO;
using OnlineVotingS.Domain.Entities;


namespace OnlineVotingS.Application.Mapping;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //CreateMap<Source, Destination>();
            CreateMap<Candidates, CandidateDTO>().ReverseMap();
            CreateMap<Elections, ElectionDTO>().ReverseMap();
            CreateMap<Votes, VoteDTO>().ReverseMap();
            CreateMap<Result, ResultDTO>().ReverseMap();
            CreateMap<Complaints, ComplaintDTO>().ReverseMap();
            CreateMap<Campaign, CampaignDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();

        }

    }
