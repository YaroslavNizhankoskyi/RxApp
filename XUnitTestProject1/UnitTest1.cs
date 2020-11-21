using Moq;
using RxApp.Data;
using RxApp.Models;
using System;
using System.Data.Entity;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var dbContextMock = new Mock<RxAppContext>();
            var dbSetMock = new Mock<DbSet<PharmGroup>>();

            PharmGroup pg = new PharmGroup
            {
                Name = "Ph"
            };

            dbSetMock.Setup(s => s.Add(pg));

            dbSetMock.Setup(s => s.Find(1)).Returns(pg);

            Console.WriteLine(dbSetMock.Setup(s => s.Find(1)).ToString());
        }
    }
}
