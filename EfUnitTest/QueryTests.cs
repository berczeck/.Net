using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace EfUnitTest
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void GetAllBlogs_orders_by_name()
        {
            var data = new List<Blog>
            {
                new Blog { Name = "BBB" },
                new Blog { Name = "ZZZ" },
                new Blog { Name = "AAA" },
            }.AsQueryable();

            var mockSet = MockRepository.GenerateMock<IDbSet<Blog>, IQueryable>();
            mockSet.Stub(x => x.Provider).Return(data.Provider);
            mockSet.Stub(x => x.Expression).Return(data.Expression);
            mockSet.Stub(x => x.ElementType).Return(data.ElementType);
            mockSet.Stub(x => x.GetEnumerator()).Return(data.GetEnumerator());

            var mockContext = MockRepository.GenerateStub<BloggingContext>();
            //mockContext.Stub(x => x.Blogs).PropertyBehavior();
            mockContext.Blogs = mockSet;

            var service = new BlogService(mockContext); 
            var blogs = service.GetAllBlogs();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name);

            //var mockSet = new Mock<DbSet<Blog>>();
            //mockSet.As<IQueryable<Blog>>().Setup(m => m.Provider).Returns(data.Provider);
            //mockSet.As<IQueryable<Blog>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Blog>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Blog>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //var mockContext = new Mock<BloggingContext>();
            //mockContext.Setup(c => c.Blogs).Returns(mockSet.Object);

            //var service = new BlogService(mockContext.Object);
            //var blogs = service.GetAllBlogs();

            //Assert.AreEqual(3, blogs.Count);
            //Assert.AreEqual("AAA", blogs[0].Name);
            //Assert.AreEqual("BBB", blogs[1].Name);
            //Assert.AreEqual("ZZZ", blogs[2].Name);
        }
    }
}
