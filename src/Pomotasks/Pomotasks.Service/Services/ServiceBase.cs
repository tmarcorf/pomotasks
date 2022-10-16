using FluentValidation;
using FluentValidation.Results;
using Pomotasks.Domain.Dtos;

namespace Pomotasks.Service.Services
{
    public abstract class ServiceBase<T, TValidator>
        where T : class
        where TValidator : AbstractValidator<T>
    {
        protected void ConfigurePagedResult(
            DtoPagedResult<T> dtoPaged,
            int skip,
            int take,
            int totalCount,
            IEnumerable<T> data,
            List<string> errorMessages = null)
        {
            dtoPaged.CurrentPage = GetCurrentPage(skip, take);
            dtoPaged.Skip = skip;
            dtoPaged.Take = take;
            dtoPaged.TotalCount = totalCount;
            dtoPaged.Data = data;

            ConfigureResult(dtoPaged, errorMessages);
        }

        protected void ConfigureSingleResult(DtoSingleResult<T> singleResult, T dto, List<string> errorMessages = null)
        {
            singleResult.DtoResult = dto;

            ConfigureResult(singleResult, errorMessages);
        }

        protected List<string> GetErrors(List<ValidationFailure> validationErrors)
        {
            List<string> errors = new();

            validationErrors.ForEach(error =>
            {
                errors.Add(error.PropertyName + ": " + error.ErrorMessage);
            });

            return errors;
        }

        protected ValidationResult Validate(T dto)
        {
            var validator = Activator.CreateInstance(typeof(TValidator));

            return (validator as TValidator)?.Validate(dto);
        }

        protected Guid GetIdAsGuid(string id)
        {
            if (Guid.TryParse(id, out Guid result))
            {
                return result;
            }

            return Guid.Empty;
        }

        protected List<Guid> GetIdsAsGuid(List<string> ids)
        {
            List<Guid> guids = new();

            ids.ForEach(id =>
            {
                if (Guid.TryParse(id, out Guid result))
                {
                    guids.Add(result);
                }
            });

            return guids;
        }

        protected void ConfigureResult(DtoResult result, List<string> errorMessages = null!)
        {
            if (errorMessages is not null && errorMessages.Count > 0)
            {
                result.Errors.AddRange(errorMessages);
            }

            result.Success = (errorMessages is null || errorMessages.Count == 0);
        }

        private static int GetCurrentPage(int skip, int take)
        {
            return skip < take ? 1 : ((skip / take) + 1);
        }
    }
}
