﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Specification
{
    public interface ISpecificationo<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
