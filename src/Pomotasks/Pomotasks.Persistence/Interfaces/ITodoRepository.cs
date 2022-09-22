﻿using Pomotasks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomotasks.Persistence.Interfaces
{
    public interface ITodoRepository : IFindRepository<Todo>
    {
    }
}