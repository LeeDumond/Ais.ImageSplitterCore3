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
            
            Stream inputStream = null;
            Stream outputStream = null;

            try
            {
                inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                TiffBitmapDecoder decoder = new TiffBitmapDecoder(inputStream, BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);

                TiffBitmapEncoder encoder = new TiffBitmapEncoder();

                foreach (int pageNumber in pageNumbers)
                {
                    try
                    {
                        encoder.Frames.Add(decoder.Frames[pageNumber]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                }

                if (encoder.Frames.Any())
                {
                    outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                    encoder.Save(outputStream);
                }
            }
            catch (Exception e)
            {
                result.ErrorStatus = e.Message;
                result.StackTrace = e.StackTrace;
            }
            finally
            {
                inputStream?.Dispose();
                outputStream?.Dispose();
            }

            return result;
        }
    }
}