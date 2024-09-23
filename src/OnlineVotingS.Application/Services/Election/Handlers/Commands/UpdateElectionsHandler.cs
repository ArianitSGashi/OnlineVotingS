using static FluentResults.Result;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineVotingS.Application.Services.Election.Requests.Commands;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Domain.Errors;
using FluentResults;

namespace OnlineVotingS.Application.Services.Election.Handlers.Commands
{
    public class UpdateElectionsHandler : IRequestHandler<UpdateElectionsCommand, Result<Elections>>
    {
        private readonly IElectionRepository _electionsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateElectionsHandler> _logger;

        public UpdateElectionsHandler(IElectionRepository electionsRepository, IMapper mapper, ILogger<UpdateElectionsHandler> logger)
        {
            _electionsRepository = electionsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Elections>> Handle(UpdateElectionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var elections = await _electionsRepository.GetByIdAsync(request.ElectionDto.ElectionID);
                if (elections == null)
                {
                    return new Result<Elections>().WithError(ErrorCodes.ELECTION_NOT_FOUND.ToString());
                }

                _mapper.Map(request.ElectionDto, elections);
                await _electionsRepository.UpdateAsync(elections);
                return Ok(elections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the election with ID {ElectionID}: {ErrorMessage}", request.ElectionDto.ElectionID, ex.Message);
                return new Result<Elections>().WithError(ErrorCodes.ELECTION_UPDATE_FAILED.ToString());
            }
        }
    }
}
