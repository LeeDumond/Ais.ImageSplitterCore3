using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Ais.ImageSplitter.Wpf
{
    public class Splitter : ISplitter
    {
        public async Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pageNumbers)
        {
            var result = new SplitResult {InputFilePath = inputFilePath};

            try
            {
                await using (Stream inputStream =
                    new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var decoder = new TiffBitmapDecoder(inputStream, BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default);

                    var encoder = new TiffBitmapEncoder();

                    foreach (int pageNumber in pageNumbers)
                    {
                        if (pageNumber > 0 && pageNumber <= decoder.Frames.Count)
                        {
                            encoder.Frames.Add(decoder.Frames[pageNumber-1]);
                        }
                    }

                    if (encoder.Frames.Any())
                    {
                        await using (Stream outputStream =
                            new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                        {
                            encoder.Save(outputStream);
                        }
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