using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Soft.Core.Caching;
using Soft.Core.Fakes;
using Soft.Test;

namespace Soft.Core.Test.Caching
{
    [TestFixture]
    public class PerRequestCacheManagerTest
    {
        [Test]
        public void Can_set_and_get_object_from_cache()
        {
            HttpContextFactory.SetCurrentContext(GetMockedHttpContext());
            var context = HttpContextFactory.Current;
            var cacheManager = new PerRequestCacheManager(context);

            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Get<int>("some_key_1").ShouldEqual(3);
        }

        [Test]
        public void Can_validate_whetherobject_is_cached()
        {
            HttpContextFactory.SetCurrentContext(GetMockedHttpContext());
            var context = HttpContextFactory.Current;
            var cacheManager = new PerRequestCacheManager(context);

            cacheManager.Set("some_key_1", 3, int.MaxValue);
            cacheManager.Set("some_key_2", 4, int.MaxValue);

            cacheManager.IsSet("some_key_1").ShouldEqual(true);
            cacheManager.IsSet("some_key_3").ShouldEqual(false);
        }


        [Test]
        public void Can_clear_cache()
        {
            HttpContextFactory.SetCurrentContext(GetMockedHttpContext());
            var context = HttpContextFactory.Current;
            var cacheManager = new PerRequestCacheManager(context);

            cacheManager.Set("some_key_1", 3, int.MaxValue);

            cacheManager.Clear();

            cacheManager.IsSet("some_key_1").ShouldEqual(false);
        }

        private HttpContextBase GetMockedHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            var urlHelper = new Mock<UrlHelper>();

            var routes = new RouteCollection();
            var requestContext = new Mock<RequestContext>();
            requestContext.Setup(x => x.HttpContext).Returns(context.Object);
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);
            context.Setup(ctx => ctx.Items).Returns(new Dictionary<String,object>());
            user.Setup(ctx => ctx.Identity).Returns(identity.Object);
            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("test");
            request.Setup(req => req.Url).Returns(new Uri("http://www.google.com"));
            request.Setup(req => req.RequestContext).Returns(requestContext.Object);
            requestContext.Setup(x => x.RouteData).Returns(new RouteData());
            request.SetupGet(req => req.Headers).Returns(new NameValueCollection());
            return context.Object;
        }
    }

    public class HttpContextFactory
    {
        private static HttpContextBase _mContext;
        public static HttpContextBase Current
        {
            get
            {
                if (_mContext != null)
                    return _mContext;

                if (HttpContext.Current == null)
                    throw new InvalidOperationException("HttpContext not available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            _mContext = context;
        }
    }
}