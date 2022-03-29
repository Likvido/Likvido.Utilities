using System.Collections.Generic;
using Likvido.Utilities.Common.Exceptions;
using Likvido.Utilities.Sorting;

namespace Likvido.Utilities.Extensions
{
    public static class OrderingQueryExtensions
    {
        public static IReadOnlyCollection<OrderByFunction<T>> GetOrderByFunctions<T>(
            this IOrderingQuery<T> orderingQuery)
        {
            var sortings = orderingQuery.Sortings;
            var orderByFunctions = sortings == null ? null : BuildOrderFunctions(orderingQuery, sortings);
            orderByFunctions = orderByFunctions?.Count > 0 ? orderByFunctions : orderingQuery.GetDefaultOrdering();
            return orderByFunctions;
        }

        private static IReadOnlyCollection<OrderByFunction<T>> BuildOrderFunctions<T>(
            IOrderingQuery<T> orderingQuery,
            IEnumerable<SortingBy> sortings)
        {
            var mappings = orderingQuery.GetOrderingPropertyMappings();
            List<OrderByFunction<T>> result = new();

            foreach (var sorting in sortings)
            {
                string property = sorting.PropertyName;
                if (!mappings.TryGetValue(property, out var selector))
                {
                    throw new OrderByFunctionBuildException(property);
                }
                result.Add(new OrderByFunction<T>(selector, sorting.IsDescending));
            }

            return result;
        }
    }
}
