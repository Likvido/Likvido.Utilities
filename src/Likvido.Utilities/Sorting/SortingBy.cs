namespace Likvido.Utilities.Sorting
{
    public class SortingBy
    {
        public string PropertyName { get; }
        public bool IsDescending { get; }

        public SortingBy(string propertyName, bool isDescending)
        {
            PropertyName = propertyName;
            IsDescending = isDescending;
        }
    }
}
