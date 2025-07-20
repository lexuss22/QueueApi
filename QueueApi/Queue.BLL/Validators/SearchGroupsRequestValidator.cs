using FluentValidation;
using Queue.DTO.Models;

namespace Queue.BLL.Validators
{
    public class SearchGroupsRequestValidator : AbstractValidator<SearchGroupsRequest>
    {
        public SearchGroupsRequestValidator()
        {
            RuleFor(x => x.City)
                .NotNull().WithMessage("City cannot be null")
                .NotEmpty().WithMessage("City cannot be empty")
                .MaximumLength(100).WithMessage("City name is too long");
            RuleFor(x => x.StartTime)
                .NotNull().WithMessage("Start time cannot be null")
                .Must(st => st != null ? IsValidTimeSpan(st) : false)
                .WithMessage("Invalid time format");
            RuleFor(x => x.FinishTime)
                .NotNull().WithMessage("Finish time cannot be null")
                .Must(ft => ft != null ? IsValidTimeSpan(ft) : false)
                .WithMessage("Invalid time format");
        }

        private bool IsValidTimeSpan(string time)
        {
            return TimeSpan.TryParse(time, out _);
        }
    }
}
