using System.Linq;
using Ais.ImageSplitter.Wpf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ais.ImageSplitter.Tests
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void PageList_1()
        {
            var request = new SplitRequest
            {
                Pages = "1,3,5"
            };

            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, request.PageList);

        }

        [TestMethod]
        public void PageList_10()
        {
            var request = new SplitRequest
            {
                Pages = "x,33,[54-57],[abc],2"
            };

            CollectionAssert.AreEqual(new[] { 2, 33, 54, 55, 56, 57 }, request.PageList);
        }

        [TestMethod]
        public void PageList_11()
        {
            var request = new SplitRequest
            {
                Pages = ""
            };

            Assert.IsFalse(request.PageList.Any());

        }

        [TestMethod]
        public void PageList_12()
        {
            var request = new SplitRequest
            {
                Pages = null
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_13()
        {
            var request = new SplitRequest
            {
                Pages = "[x-10]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_14()
        {
            var request = new SplitRequest
            {
                Pages = "[-13]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_15()
        {
            var request = new SplitRequest
            {
                Pages = "[3346-]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_16()
        {
            var request = new SplitRequest
            {
                Pages = "[-]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_17()
        {
            var request = new SplitRequest
            {
                Pages = "[]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_18()
        {
            var request = new SplitRequest
            {
                Pages = "-"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_19()
        {
            var request = new SplitRequest
            {
                Pages = "[-"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_2()
        {
            var request = new SplitRequest
            {
                Pages = "[1-3],7"
            };

            CollectionAssert.AreEqual(new[] { 1, 2, 3, 7 }, request.PageList);
        }

        [TestMethod]
        public void PageList_20()
        {
            var request = new SplitRequest
            {
                Pages = "-]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_21()
        {
            var request = new SplitRequest
            {
                Pages = "["
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_22()
        {
            var request = new SplitRequest
            {
                Pages = "]"
            };

            Assert.IsFalse(request.PageList.Any());
        }

        [TestMethod]
        public void PageList_3()
        {
            var request = new SplitRequest
            {
                Pages = "[1-2],[4-6],[9-12]"
            };

            CollectionAssert.AreEqual(new[] { 1, 2, 4, 5, 6, 9, 10, 11, 12 }, request.PageList);
        }

        [TestMethod]
        public void PageList_4()
        {
            var request = new SplitRequest
            {
                Pages = "5,5,5"
            };

            CollectionAssert.AreEqual(new[] { 5 }, request.PageList);
        }

        [TestMethod]
        public void PageList_5()
        {
            var request = new SplitRequest
            {
                Pages = "5,3,12"
            };

            CollectionAssert.AreEqual(new[] { 3, 5, 12 }, request.PageList);
        }

        [TestMethod]
        public void PageList_6()
        {
            var request = new SplitRequest
            {
                Pages = "5,3,5"
            };

            CollectionAssert.AreEqual(new[] { 3, 5 }, request.PageList);
        }

        [TestMethod]
        public void PageList_7()
        {
            var request = new SplitRequest
            {
                Pages = "[300-304]"
            };

            CollectionAssert.AreEqual(new[] { 300, 301, 302, 303, 304 }, request.PageList);
        }

        [TestMethod]
        public void PageList_8()
        {
            var request = new SplitRequest
            {
                Pages = "214,[300-304],11"
            };

            CollectionAssert.AreEqual(new[] { 11, 214, 300, 301, 302, 303, 304 }, request.PageList);
        }

        [TestMethod]
        public void PageList_9()
        {
            var request = new SplitRequest
            {
                Pages = "some nonsense"
            };

            Assert.IsFalse(request.PageList.Any());
        }
    }
}
