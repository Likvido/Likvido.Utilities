using System;
using System.Collections.Generic;
using System.Linq;
using Likvido.Utilities.Tests.Helpers;

namespace Likvido.Utilitites.Tests.Helpers
{
    public static class QueryExtensionsTestsHelper
    {
        private static readonly Random Random = new Random();
        public static List<Dummy> CreateRandomDummyList(int count)
        {
            var timestamp = DateTime.UtcNow;
            List<Dummy> dummies = new List<Dummy>();
            for (int i = 0; i < count; i++)
            {
                dummies.Add(GetRandomDummy(timestamp));
            }
            return dummies;
        }

        private static Dummy GetRandomDummy(DateTime timestamp)
        {
            return new Dummy
            {
                IntProperty = Random.Next(20),
                StringProperty = RandomString(1),
                NullableDateTime = Random.Next(3) == 0 ? null : timestamp.Subtract(TimeSpan.FromMinutes(Random.Next(20)))
            };
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
