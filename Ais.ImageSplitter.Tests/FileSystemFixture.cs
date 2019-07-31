using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;

namespace Ais.ImageSplitter.Tests
{
    public class FileSystemFixture
    {
        public FileSystemFixture()
        {
            byte[] sampleInputContents = File.ReadAllBytes("UM0005179.tif");

            FileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"C:\images\sample-input.tif", new MockFileData(sampleInputContents)}
            });
        }

        public MockFileSystem FileSystem { get; }
    }
}