using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

[assembly: InternalsVisibleTo("Ais.ImageSplitter.Tests")]

namespace Ais.ImageSplitter.Wpf
{
    public class Splitter : ISplitter
    {
        private readonly IFileSystem _fileSystem;
        internal TiffBitmapEncoder Encoder { get; private set; }

        public Splitter() : this(new FileSystem())
        {
            
        }

        internal Splitter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pageNumbers)
        {
            SplitResult result = new SplitResult();

            try
            {
                if (inputFilePath == null)
                {
                    throw new ArgumentNullException(nameof(inputFilePath));
                }

                if (outputFilePath == null)
                {
                    throw new ArgumentNullException(nameof(outputFilePath));
                }

                if (pageNumbers == null)
                {
                    throw new ArgumentNullException(nameof(pageNumbers));
                }

                result.InputFilePath = inputFilePath;

                await using (Stream inputStream = _fileSystem.FileStream.Create(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var decoder = new TiffBitmapDecoder(inputStream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default);

                    Encoder = new TiffBitmapEncoder();

                    foreach (int pageNumber in pageNumbers)
                    {
                        if (pageNumber > 0 && pageNumber <= decoder.Frames.Count)
                        {
                            Encoder.Frames.Add(decoder.Frames[pageNumber-1]);
                        }
                    }

                    if (Encoder.Frames.Any())
                    {
                        await using (Stream outputStream = _fileSystem.FileStream.Create(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            Encoder.Save(outputStream);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("No pages were saved to the output. This may be because no pages were indicated, or the indicated pages were not found in the source file.");
                    }
                }
            }
            catch (Exception e)
            {
                result.ErrorStatus = e.Message;
                result.StackTrace = e.StackTrace;
            }

            return result;
        }
    }
}