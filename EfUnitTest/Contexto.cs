using System.Collections.Generic;
using System.Data.Entity;

namespace EfUnitTest
{
    public class BloggingContext : DbContext
    {
        public virtual IDbSet<Blog> Blogs { get; set; }
        public virtual IDbSet<Post> Posts { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
