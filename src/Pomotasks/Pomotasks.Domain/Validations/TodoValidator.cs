using FluentValidation;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Globalization;

namespace Pomotasks.Domain.Validations
{
    public class TodoValidator : AbstractValidator<DtoTodo>
    {
        private const int TITLE_MAX_LENGTH = 70;
        private const int DETAILS_MAX_LENGTH = 300;

        public TodoValidator()
        {
            TitleRules();
            DetailsRules();
            CreationDateRules();
            UserIdRules();
        }

        private void TitleRules()
        {
            var maxLenghtMessage = string.Format(Message.GetMessage("8"), TITLE_MAX_LENGTH);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage(Message.GetMessage("14"))
                .Must(x => x.Length <= TITLE_MAX_LENGTH)
                .WithMessage(maxLenghtMessage)
                .WithName(Message.GetMessage("13"));
        }

        private void DetailsRules()
        {
            var maxLenghtMessage = string.Format(Message.GetMessage("8"), DETAILS_MAX_LENGTH);

            RuleFor(x => x.Details)
                .NotNull()
                .NotEmpty()
                .WithMessage(Message.GetMessage("7"))
                .Must(x => x.Length <= DETAILS_MAX_LENGTH)
                .WithMessage(maxLenghtMessage)
                .WithName(Message.GetMessage("6"));
        }

        private void CreationDateRules()
        {
            RuleFor(x => x.CreationDate)
                .NotNull()
                .NotEmpty()
                .WithMessage(Message.GetMessage("5"))
                .Must(x => x.Date <= DateTime.Now)
                .WithMessage(Message.GetMessage("4"))
                .WithName(Message.GetMessage("20"));
        }

        private void UserIdRules()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage(Message.GetMessage("16"))
                .WithName(Message.GetMessage("15"));
        }
    }
}
