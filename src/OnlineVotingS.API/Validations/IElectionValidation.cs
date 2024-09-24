using FluentResults;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

namespace OnlineVotingS.API.Validations;

public interface IElectionValidation
{
    Task<Result> ValidateElectionAsync(IElectionViewModel model);
}