using System;
using NUnit.Framework;
using Soft.Core.Infrastructure.Patterns;
using Soft.Test;

namespace Soft.Core.Test.Infrastructure.Patterns
{
    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void Singleton_IsNullByDefault()
        {
            var instance = Singleton<SingletonTests>.Instance;
            instance.ShouldBeNull();
        }

        [Test]
        public void Singletons_ShareSame_SingletonsDictionary()
        {
            Singleton<int>.Instance = 1;
            Singleton<double>.Instance = 2.0;

            Singleton.AllSingletons[typeof (int)].ShouldEqual(1);
            Singleton.AllSingletons[typeof (double)].ShouldEqual(2.0);
            Singleton.AllSingletons.ShouldBeTheSameAs(Singleton.AllSingletons);

        }

        [Test]
        public void SingletonDictionary_IsCreatedByDefault()
        {
            var instance = SingletonDictionary<SingletonTests, object>.Instance;
            instance.ShouldNotBeNull();
        }

        [Test]
        public void SingletonDictionary_CanStoreStuff()
        {
            var instance = SingletonDictionary<Type, SingletonTests>.Instance;
            instance[typeof(SingletonTests)] = this;
            instance[typeof(SingletonTests)].ShouldBeTheSameAs(this);
        }

        [Test]
        public void SingletonList_IsCreatedByDefault()
        {
            var instance = SingletonList<SingletonTests>.Instance;
            instance.ShouldNotBeNull();
        }

        [Test]
        public void SingletonList_CanStoreItems()
        {
            var instance = SingletonList<SingletonTests>.Instance;
            instance.Insert(0, this);
            instance[0].ShouldBeTheSameAs(this);
        }
    }
}