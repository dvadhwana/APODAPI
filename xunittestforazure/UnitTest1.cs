using System;
using Xunit;
using azuretestapp.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace xunittestforazure
{
    public class UnitTest1
    {
        //private Mock<HttpContent> _mockHttpContext;
        [Fact]
        public void Test1()
        {
            Assert.Equal("PASS", "PASS");
        }
    }
}
