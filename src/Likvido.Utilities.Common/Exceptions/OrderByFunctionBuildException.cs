using System;

namespace Likvido.Utilities.Common.Exceptions
{
    public class OrderByFunctionBuildException : Exception
    {
        public OrderByFunctionBuildException()
        {
        }

        public OrderByFunctionBuildException(string key) : base($"Could not build OrderBy function. Unknown key: {key}")
        {
        }

        public OrderByFunctionBuildException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
