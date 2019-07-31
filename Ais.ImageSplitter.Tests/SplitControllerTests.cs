using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ais.ImageSplitter.Api.Controllers;
using Ais.ImageSplitter.Wpf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ais.ImageSplitter.Tests
{


    [TestClass]
    public class SplitControllerTests
    {
        [TestMethod]
        public async Task Post_ReturnsResultWithStatusCode200()
        {
            var mockSplitter = new Mock<ISplitter>();
            mockSplitter.Setup(s => s.SplitAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int[]>()))
                .ReturnsAsync(() => new SplitResult());

            var controller = new SplitController(mockSplitter.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };

            ActionResult<SplitResult> result = await controller.Post(new SplitRequest { });

            Assert.AreEqual(200, controller.Response.StatusCode);
            Assert.IsNull(result.Value.ErrorStatus);
            Assert.IsNull(result.Value.StackTrace);
        }


        [TestMethod]
        public async Task Post_WithError_ReturnsResultWithStatusCode500()
        {
            var mockSplitter = new Mock<ISplitter>();
            mockSplitter.Setup(s => s.SplitAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int[]>()))
                .ReturnsAsync( () => new SplitResult{ErrorStatus = "some error message", StackTrace = "some stack trace"});

            var controller = new SplitController(mockSplitter.Object)
            {
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
            };

            ActionResult<SplitResult> result = await controller.Post(new SplitRequest{InputFilePath = "some input path"});

            Assert.AreEqual(500, controller.Response.StatusCode);
            Assert.AreEqual("some error message", result.Value.ErrorStatus);
            Assert.AreEqual("some stack trace", result.Value.StackTrace);
        }
    }
}
