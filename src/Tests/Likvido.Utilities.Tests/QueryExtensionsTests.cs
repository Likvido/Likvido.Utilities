using System;
using System.Collections.Generic;
using System.Linq;
using Likvido.BankReading.Utilities.Extensions;
using Likvido.Utilities.Sorting;
using Likvido.Utilities.Tests.Helpers;
using Likvido.Utilitites.Tests.Helpers;
using Shouldly;
using Xunit;

namespace Likvido.Utilities.Tests
{
    [Collection("QueryExtensionsTests")]
    public class QueryExtensionsTests
    {
        [Fact]
        public void ApplyOrderByFunctions_ThreeProperties()
        {
            var dummies = QueryExtensionsTestsHelper.CreateRandomDummyList(50).AsQueryable();
            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.IntProperty, false),
                new OrderByFunction<Dummy>(x => x.StringProperty, true),
                new OrderByFunction<Dummy>(x => x.NullableDateTime, true)
            };

            var ordered = dummies.ApplyOrderByFunctions(orderFunctions);
            var orderedFact = dummies.OrderBy(x => x.IntProperty).ThenByDescending(x => x.StringProperty).ThenByDescending(x => x.NullableDateTime);

            ordered.ShouldBe(orderedFact);
        }

        [Fact]
        public void ApplyOrderByFunctions_TwoProperties()
        {
            var dummies = QueryExtensionsTestsHelper.CreateRandomDummyList(50).AsQueryable();
            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.IntProperty, false),
                new OrderByFunction<Dummy>(x => x.StringProperty, true)
            };

            var ordered = dummies.ApplyOrderByFunctions(orderFunctions);
            var orderedFact = dummies.OrderBy(x => x.IntProperty).ThenByDescending(x => x.StringProperty);

            ordered.ShouldBe(orderedFact);
        }

        [Fact]
        public void ApplyOrderByFunctions_OneProperty()
        {
            var dummies = QueryExtensionsTestsHelper.CreateRandomDummyList(50).AsQueryable();
            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.StringProperty, true)
            };

            var ordered = dummies.ApplyOrderByFunctions(orderFunctions);
            var orderedFact = dummies.OrderByDescending(x => x.StringProperty);

            ordered.ShouldBe(orderedFact);
        }

        [Fact]
        public void ApplyOrderByFunctions_NullablePropertyAscending()
        {
            var dummies = QueryExtensionsTestsHelper.CreateRandomDummyList(50).AsQueryable();
            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.NullableDateTime, false)
            };

            var ordered = dummies.ApplyOrderByFunctions(orderFunctions);
            var orderedFact = dummies.OrderBy(x => x.NullableDateTime);

            ordered.ShouldBe(orderedFact);
        }

        [Fact]
        public void ApplyOrderByFunctions_NullablePropertyDescending()
        {
            var dummies = QueryExtensionsTestsHelper.CreateRandomDummyList(50).AsQueryable();
            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.NullableDateTime, true)
            };

            var ordered = dummies.ApplyOrderByFunctions(orderFunctions);
            var orderedFact = dummies.OrderByDescending(x => x.NullableDateTime);

            ordered.ShouldBe(orderedFact);
        }

        [Fact]
        public void ApplyOrderByFunctions_NullablePropertyDescending_NullsInTheEnd()
        {
            var dummyNullableFirst = new List<Dummy>
            {
                new Dummy(),
                new Dummy
                {
                    NullableDateTime = DateTime.Now
                }
            }.AsQueryable();

            var orderFunctions = new List<OrderByFunction<Dummy>>()
            {
                new OrderByFunction<Dummy>(x => x.NullableDateTime, true)
            };

            var ordered = dummyNullableFirst.ApplyOrderByFunctions(orderFunctions);

            ordered.First().NullableDateTime.ShouldNotBeNull();
        }
    }
}
