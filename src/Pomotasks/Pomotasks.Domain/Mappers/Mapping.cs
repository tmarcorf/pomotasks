using AutoMapper;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Interfaces;

namespace Pomotasks.Domain.Mappers
{
    public class Mapping<TEntity, TDto> : IMapping<TEntity, TDto>
        where TEntity : EntityBase
        where TDto : DtoBase
    {
        protected IMapper mapper = null;

        public Mapping()
        {
            InitializeMapper();
        }

        public virtual TDto GetDto(TEntity entity)
        {
            if (entity is not null)
            {
                return mapper.Map<TDto>(entity);
            }

            return null;
        }

        public virtual IEnumerable<TDto> GetDtos(IEnumerable<TEntity> entities)
        {
            if (entities is not null && entities.Any())
            {
                return mapper.Map<IEnumerable<TDto>>(entities);
            }

            return null;
        }

        public virtual IEnumerable<TEntity> GetEntities(IEnumerable<TDto> dtos)
        {
            if (dtos is not null && dtos.Any())
            {
                return mapper.Map<IEnumerable<TEntity>>(dtos);
            }

            return null;
        }

        public virtual TEntity GetEntity(TDto dto)
        {
            if (dto is not null)
            {
                return mapper.Map<TEntity>(dto);
            }

            return null;
        }

        private void InitializeMapper()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<TEntity, TDto>().ReverseMap();
            });

            mapper = new Mapper(configuration);
        }
    }
}
