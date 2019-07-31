using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ais.ImageSplitter.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ais.ImageSplitter.Tests
{
    [TestClass]
    public class SplitterTests
    {
        [TestMethod]
        public async Task SplitAsync_InputFilePathIsSame()
        {
            var splitter = new Splitter();

            var result = await splitter.SplitAsync("some input path", null, null);

            Assert.AreEqual("some input path", result.InputFilePath);
        }

        [TestMethod]
        public async Task SplitAsync_NullInputPathThrows()
        {
            var splitter = new Splitter();

            var result = await splitter.SplitAsync(null, null, null);

            Assert.AreEqual("Path cannot be null. (Parameter 'path')", result.ErrorStatus);
        }
    }
}
