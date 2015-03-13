using System;
using NUnit.Framework;
using Soft.Core.Infrastructure;
using Soft.Test;

namespace Soft.Core.Test
{
    public abstract class TypeFindingBase : TestsBase
    {
        protected ITypeFinder TypeFinder;

        protected abstract Type[] GetTypes();

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            TypeFinder = new Fakes.FakeTypeFinder(typeof(TypeFindingBase).Assembly, GetTypes());
        }
    }
}
