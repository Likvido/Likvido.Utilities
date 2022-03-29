using System;

namespace Likvido.Utilities.Tests.Helpers
{
    public record Dummy
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; } = default!;
        public DateTime? NullableDateTime { get; set; }
    }
}
