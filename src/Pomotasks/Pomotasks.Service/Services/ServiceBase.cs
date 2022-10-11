using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Service.Services
{
    public abstract class ServiceBase<T, TValidator>
        where T : class
        where TValidator : AbstractValidator<T>
    {
        private ValidationResult _validationResult;

        protected DtoPaged<T> ConvertToPaginatedResult(
            int skip,
            int take,
            int totalCount,
            IEnumerable<T> data)
        {
            var currentPage = skip < take ? 1 : ((skip / take) + 1);

            return new DtoPaged<T>
            {
                CurrentPage = currentPage,
                Skip = skip,
                Take = take,
                TotalCount = totalCount,
                Data = data
            };
        }

        public ValidationResult Validate(T dto)
        {
            var validator = Activator.CreateInstance(typeof(TValidator));

            return (validator as TValidator).Validate(dto);
        }

        public Guid GetIdAsGuid(string id)
        {
            Guid result;

            if (Guid.TryParse(id, out result))
            {
                return result;
            }

            return Guid.Empty;
        }

        public List<Guid> GetIdsAsGuid(List<string> ids)
        {
            List<Guid> guids = new List<Guid>();

            ids.ForEach(id =>
            {
                Guid result;
                if (Guid.TryParse(id, out result))
                {
                    guids.Add(result);
                }
            });

            return guids;
        }

        private List<string> GetErrors(List<ValidationFailure> validationErrors)
        {
            List<string> errors = new();

            validationErrors.ForEach(error =>
            {
                errors.Add(error.PropertyName + ": " + error.ErrorMessage + Environment.NewLine);
            });

            return errors;
        }
    }
}
