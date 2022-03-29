using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Likvido.Utilities.Sorting
{
    public interface IOrderingQuery<TEntity> : ISortingContainer
    {
        IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> GetOrderingPropertyMappings();
        IReadOnlyCollection<OrderByFunction<TEntity>> GetDefaultOrdering();
    }
}
