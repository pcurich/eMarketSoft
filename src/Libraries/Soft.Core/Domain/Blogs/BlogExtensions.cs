using System;
using System.Collections.Generic;

namespace Soft.Core.Domain.Blogs
{
    public static class BlogExtensions
    {
        /// <summary>
        /// Un blog tiene una lista de tags que son cadenas 
        /// separadas por comas
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public static string[] ParseTags(this BlogPost blogPost)
        {
            if (blogPost == null)
                throw new ArgumentNullException("blogPost");

            var parsedTags = new List<string>();

            if (String.IsNullOrEmpty(blogPost.Tags))
                return parsedTags.ToArray();

            string[] tags2 = blogPost.Tags.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string tag2 in tags2)
            {
                var tmp = tag2.Trim();
                if (!String.IsNullOrEmpty(tmp))
                    parsedTags.Add(tmp);
            }

            return parsedTags.ToArray();
        }
    }
}