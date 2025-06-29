using Application.DTOs;
using FluentValidation;
using System;

namespace Application.Validators
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.DueDate)
                .Must(BeAFutureDate).WithMessage("Due date must be in the future");

            RuleFor(x => x.Priority)
                .Must(BeAValidPriority).WithMessage("Invalid priority. Use Low, Medium, or High.");

            RuleFor(x => x.Status)
                .Must(BeAValidStatus).WithMessage("Invalid status. Use Pending, InProgress, or Done.");
        }

        private bool BeAFutureDate(DateTime dueDate)
        {
            return dueDate > DateTime.Now;
        }

        private bool BeAValidPriority(string priority)
        {
            return new[] { "Low", "Medium", "High" }.Contains(priority);
        }

        private bool BeAValidStatus(string status)
        {
            return new[] { "Pending", "InProgress", "Done" }.Contains(status);
        }
    }
}
