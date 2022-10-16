namespace Pomotasks.Domain.Interfaces
{
    public interface IMapping<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        TEntity GetEntity(TDto dto);

        TDto GetDto(TEntity entity);

        IEnumerable<TEntity> GetEntities(IEnumerable<TDto> dtos);

        IEnumerable<TDto> GetDtos(IEnumerable<TEntity> entities);
    }
}
