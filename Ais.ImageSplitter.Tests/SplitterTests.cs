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
        private readonly FileSystemFixture _fileSystemFixture;

        public SplitterTests()
        {
            _fileSystemFixture = new FileSystemFixture();
        }


        [TestMethod]
        public async Task SplitAsync_InputFilePathIsSame()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-input.tif", new[] { 1, 2, 3 });

            Assert.AreEqual(@"C:\images\sample-input.tif", result.InputFilePath);
        }

        [TestMethod]
        public async Task SplitAsync_NullInputPathThrows()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(null, @"C:\images\sample-output.tif", new[]{1,2,3});

            Assert.AreEqual("Value cannot be null. (Parameter 'inputFilePath')", result.ErrorStatus);
        }

        [TestMethod]
        public async Task SplitAsync_NullOutputPathThrows()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", null, new[] { 1, 2, 3 });

            Assert.AreEqual("Value cannot be null. (Parameter 'outputFilePath')", result.ErrorStatus);
        }

        [TestMethod]
        public async Task SplitAsync_NullPageNumbersThrows()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-output.tif", null);

            Assert.AreEqual("Value cannot be null. (Parameter 'pageNumbers')", result.ErrorStatus);
        }

        [TestMethod]
        public async Task SplitAsync_EmptyPageNumbersThrows()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-output.tif", new int[]{});

            Assert.AreEqual("No pages were saved to the output. This may be because no pages were indicated, or the indicated pages were not found in the source file.", result.ErrorStatus);
        }

        [TestMethod]
        public async Task SplitAsync_NoValidPageNumbersThrows()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-output.tif", new int[] { 0, -12, 61, 65});

            Assert.AreEqual("No pages were saved to the output. This may be because no pages were indicated, or the indicated pages were not found in the source file.", result.ErrorStatus);
        }

        [TestMethod]
        public async Task SplitAsync_InvalidPageNumbersSkipped()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-output.tif", new int[] { 0, -12, 1, 4, 6, 9, 61, 65 });

            Assert.AreEqual(4, splitter.Encoder.Frames.Count);
        }

        [TestMethod]
        public async Task SplitAsync_AllValidPageNumbersIncluded()
        {
            var splitter = new Splitter(_fileSystemFixture.FileSystem);

            SplitResult result = await splitter.SplitAsync(@"C:\images\sample-input.tif", @"C:\images\sample-output.tif", new int[] { 1, 4, 6, 9, 14,15,16 });

            Assert.AreEqual(7, splitter.Encoder.Frames.Count);
        }
    }
}
