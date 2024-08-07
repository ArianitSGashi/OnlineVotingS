using AutoMapper;
using OnlineVotingS.Application.DTO;
using OnlineVotingS.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineVotingS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from Campaign entity to CampaignDTO
            CreateMap<Campaign, CampaignDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ElectionID, opt => opt.MapFrom(src => src.ElectionID))
                .ForMember(dest => dest.CandidateID, opt => opt.MapFrom(src => src.CandidateID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore UpdatedAt as it is not in DTO

            // Map from CampaignDTO to Campaign entity
            CreateMap<CampaignDTO, Campaign>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ElectionID, opt => opt.MapFrom(src => src.ElectionID))
                .ForMember(dest => dest.CandidateID, opt => opt.MapFrom(src => src.CandidateID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore UpdatedAt as it will be set by the database
        }
    }
}
