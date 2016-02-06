using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace EfUnitTest
{
    //http://msdn.microsoft.com/en-us/library/dn314429.aspx

    [TestClass]
    public class NonQueryTests
    {
        [TestMethod]
        public void CreateBlog_saves_a_blog_via_context()
        {
            //var mockSet = new Mock<DbSet<Blog>>();

            //var mockContext = new Mock<BloggingContext>();
            //mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);

            //var service = new BlogService(mockContext.Object);
            //service.AddBlog("ADO.NET Blog", "http://blogs.msdn.com/adonet");

            //mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());

            var mockSet = MockRepository.GenerateStub<DbSet<Blog>>();
            mockSet.Stub(x => x.Add(null)).IgnoreArguments().Repeat.Once();

            var mockContext = MockRepository.GenerateStub<BloggingContext>();
//            mockContext.Stub(x => x.Blogs).Return(mockSet);
            mockContext.Blogs = mockSet;
            mockContext.Stub(x => x.SaveChanges()).Return(1).Repeat.Once();

            var repositorio = new BlogService(mockContext);
            repositorio.AddBlog("ADO.NET Blog", "http://blogs.msdn.com/adonet");

            mockSet.VerifyAllExpectations();
            mockContext.VerifyAllExpectations();
        }
    }
}
