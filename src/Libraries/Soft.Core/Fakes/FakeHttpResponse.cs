﻿using System.Text;
using System.Web;

namespace Soft.Core.Fakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection _cookies;
        private readonly StringBuilder _outputString = new StringBuilder();
        public override int StatusCode { get; set; }
        public override string RedirectLocation { get; set; }

        public FakeHttpResponse()
        {
            _cookies = new HttpCookieCollection();
        }

        public string ResponseOutput
        {
            get { return _outputString.ToString(); }
        }

        public override void Write(string s)
        {
            _outputString.Append(s);
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }

        public override HttpCookieCollection Cookies
        {
            get { return _cookies; }
        }
    }
}