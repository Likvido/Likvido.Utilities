using System.Collections.Generic;

namespace Likvido.Utilities.Sorting
{
    public interface ISortingContainer
    {
        IReadOnlyCollection<SortingBy>? Sortings { get; }
        IReadOnlySet<string> GetPropertyKeys();
    }
}
