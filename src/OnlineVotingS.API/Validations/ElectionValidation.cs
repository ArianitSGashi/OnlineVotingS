using FluentResults;
using OnlineVotingS.API.Models.AdminViewModels.ElectionViewModels;

namespace OnlineVotingS.API.Validations;

public class ElectionValidation : IElectionValidation
{
    public Task<Result> ValidateElectionAsync(IElectionViewModel model)
    {
        var result = new Result();
        var now = DateOnly.FromDateTime(DateTime.Now);

        if (model.StartDate < now)
        {
            result.WithError("Start date must be in the future.");
        }
        if (model.EndDate < model.StartDate)
        {
            result.WithError("End date must be after start date.");
        }
        if (model.StartDate == model.EndDate && model.EndTime <= model.StartTime)
        {
            result.WithError("End time must be after start time when dates are the same.");
        }

        return Task.FromResult(result);
    }
}