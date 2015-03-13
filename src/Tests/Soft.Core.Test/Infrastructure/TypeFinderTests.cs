using System;
using System.Linq;
using NUnit.Framework;
using Soft.Core.Infrastructure;
using Soft.Test;

namespace Soft.Core.Test.Infrastructure
{
    [TestFixture]
    public class TypeFinderTests
    {
        [Test]
        public void TypeFinder_Benchmark_Findings()
        {
            var finder = new AppDomainTypeFinder();

            var type = finder.FindClassesOfType<SomeClass>();
            var enumerable = type as Type[] ?? type.ToArray();
            enumerable.Count().ShouldEqual(1);

            typeof(ISomeInterface).IsAssignableFrom(enumerable.FirstOrDefault()).ShouldBeTrue();
        }

        public interface ISomeInterface
        {
        }

        public class SomeClass : ISomeInterface
        {
        }
    }
}
