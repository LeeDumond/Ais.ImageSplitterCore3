using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Ais.ImageSplitter.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ais.ImageSplitter.Tests
{
    [TestClass]
    public class VersionControllerTests
    {
        [TestMethod]
        public void Get_ReturnsVersion()
        {
            string versionFromAssembly = typeof(Api.Program).Assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            var controller = new VersionController();

            string version = controller.Get();

            Assert.AreEqual($"Ais.ImageSplitter.Api v{versionFromAssembly}", version);
        }
    }
}
