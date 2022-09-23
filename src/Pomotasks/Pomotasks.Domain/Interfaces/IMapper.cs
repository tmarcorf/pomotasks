using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Interfaces
{
    public interface IMapper<TEntity, TDto> 
        where TEntity : class 
        where TDto : class
    {
        TEntity GetEntity(TDto dto);

        TDto GetDto(TEntity entity);

        IEnumerable<TEntity> GetEntities(IEnumerable<TDto> dtos);

        IEnumerable<TDto> GetDtos(IEnumerable<TEntity> entities);
    }
}
