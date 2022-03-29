using System;
using System.Collections.Generic;
using System.Linq;
using Likvido.Utilities.Common.Exceptions;

namespace Likvido.Utilities.Sorting
{
    public static class SortingsHelper
    {
        public static IReadOnlyCollection<SortingBy> ParseSortings(string? query, char separator = ',', char descensionSign = '-')
        {
            if (query == null)
            {
                return Array.Empty<SortingBy>();
            }

            var keys = query.Split(separator);
            var sortings = keys.Where(x => x.Length > 0)
                .Select(x =>
                {
                    bool isDesending = x.StartsWith(descensionSign);
                    return new SortingBy(isDesending ? x[1..] : x, isDesending);
                }).ToArray();

            return sortings;
        }

        public static void ValidateSortings(ISortingContainer sortingContainer)
        {
            if (sortingContainer.Sortings?.Count > 0)
            {
                var propertyKeys = sortingContainer.GetPropertyKeys();

                var unknownKey = sortingContainer.Sortings.FirstOrDefault(x => !propertyKeys.Contains(x.PropertyName));
                if (unknownKey != null)
                {
                    throw new InvalidSortingQueryStringException(propertyKeys);
                }

                var duplicateKey = sortingContainer.Sortings.GroupBy(x => x.PropertyName).FirstOrDefault(x => x.Count() > 1);
                if (duplicateKey != null)
                {
                    throw new DuplicateSortingException(duplicateKey.Key);
                }
            }
        }
    }
}