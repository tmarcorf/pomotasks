using FluentValidation;
using Pomotasks.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("O título é obrigatório.")
                .Must(x => x.Length <= TITLE_MAX_LENGTH)
                .WithMessage($"O tamanho máximo permitido é de {TITLE_MAX_LENGTH} caracteres.")
                .WithName("Title");
        }

        private void DetailsRules()
        {
            RuleFor(x => x.Details)
                .NotNull()
                .NotEmpty()
                .WithMessage("Os detalhes são obrigatório.")
                .Must(x => x.Length <= DETAILS_MAX_LENGTH)
                .WithMessage($"O tamanho máximo permitido é de {DETAILS_MAX_LENGTH} caracteres.")
                .WithName("Details");
        }

        private void CreationDateRules()
        {
            RuleFor(x => x.CreationDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("A data de criação é obrigatória.")
                .Must(x => x.Date <= DateTime.Now)
                .WithMessage("A data de criação não pode ser superior à data atual")
                .WithName("CreationDate");
        }

        private void UserIdRules()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Id do usuário é obrigatório.")
                .WithName("UserId");
        }
    }
}
