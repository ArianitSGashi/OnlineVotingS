using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

namespace OnlineVotingS.API.Validations;

public interface IElectionValidation
{
    Task<(bool IsValid, List<string> Errors)> ValidateElectionAsync(IElectionViewModel model);
}