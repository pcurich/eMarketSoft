﻿using NUnit.Framework;
using Soft.Test;

namespace Soft.Core.Test
{
    [TestFixture]
    public class CommonHelperTests
    {
        [Test]
        public void Can_get_typed_value()
        {
            CommonHelper.To<int>("1000").ShouldBe<int>();
            CommonHelper.To<int>("1000").ShouldEqual(1000);
            CommonHelper.To<string>("PEDRO").ShouldBe<string>();
        }
    }
}
