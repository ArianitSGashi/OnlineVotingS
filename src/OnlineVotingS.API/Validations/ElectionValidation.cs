using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

namespace OnlineVotingS.API.Validations;

public class ElectionValidation : IElectionValidation
{
    public Task<(bool IsValid, List<string> Errors)> ValidateElectionAsync(IElectionViewModel model)
    {
        var errors = new List<string>();
        var now = DateOnly.FromDateTime(DateTime.Now);

        if (model.StartDate < now)
        {
            errors.Add("Start date must be in the future.");
        }

        if (model.EndDate < model.StartDate)
        {
            errors.Add("End date must be after start date.");
        }

        if (model.StartDate == model.EndDate && model.EndTime <= model.StartTime)
        {
            errors.Add("End time must be after start time when dates are the same.");
        }

        return Task.FromResult((errors.Count == 0, errors));
    }
}