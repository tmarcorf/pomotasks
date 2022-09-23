using AutoMapper;
using Pomotasks.Domain.Dtos;
using Pomotasks.Domain.Entities;
using Pomotasks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Domain.Mapping
{
    public class TodoMapper : MapperConfiguration, IMapper<Todo, DtoTodo>
    {
        private readonly IMapper _mapper;

        public TodoMapper(MapperConfigurationExpression configuration) 
            : base(configuration)
        {
            configuration.CreateMap<Todo, DtoTodo>().ReverseMap();

            _mapper = CreateMapper();
        }

        public Todo GetEntity(DtoTodo dto)
        {
            if (dto is not null)
            {
                return _mapper.Map<Todo>(dto);
            }

            return null;
        }

        public IEnumerable<Todo> GetEntities(IEnumerable<DtoTodo> dtos)
        {
            if (dtos is not null && dtos.Any())
            {
                return _mapper.Map<IEnumerable<Todo>>(dtos);
            }

            return null;
        }

        public DtoTodo GetDto(Todo entity)
        {
            if (entity is not null)
            {
                return _mapper.Map<DtoTodo>(entity);
            }

            return null;
        }

        public IEnumerable<DtoTodo> GetDtos(IEnumerable<Todo> entities)
        {
            if (entities is not null && entities.Any())
            {
                return _mapper.Map<IEnumerable<DtoTodo>>(entities);
            }

            return null;
        }
    }
}
