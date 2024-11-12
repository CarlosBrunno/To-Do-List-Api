using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using To_Do_List.Domain.Entities;


namespace To_Do_List.Domain.Contracts
{
    public class TaskItemValidator : AbstractValidator<TaskItem>
    { 
        public TaskItemValidator()
        {
            RuleFor(taskItem => taskItem.Id)
                .NotEmpty().WithMessage("Title can't be empty")
                .Must(id => id != Guid.Empty).WithMessage("Id need be valid GUID");

            RuleFor(taskItem => taskItem.Title)
                 .NotEmpty().WithMessage("Title can't be empty")
                 .MinimumLength(3).WithMessage("Title can't have less of 3 characters");

            RuleFor(taskItem => taskItem.IsChecked)
                 .NotNull().WithMessage("IsChecked can't be null.");

            RuleFor(taskItem => taskItem.CreatedAt)
                .NotEmpty().WithMessage("CreatedAt can't be empty")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt can't be in the future");

            RuleFor(taskItem => taskItem.UpdatedAt)
                .GreaterThanOrEqualTo(taskItem => taskItem.CreatedAt).When(taskItem => taskItem.UpdatedAt.HasValue)
                .WithMessage("The UpdatedAt must be greater than or equal to the CreatedAt")
                .LessThanOrEqualTo(DateTime.UtcNow).When(taskItem => taskItem.UpdatedAt.HasValue)
                .WithMessage("CreatedAt can't be in the past");

        }
    }
}



