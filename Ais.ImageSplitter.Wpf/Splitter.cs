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
            var result = new SplitResult { InputFilePath = inputFilePath };

            try
            {
                Stream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                TiffBitmapDecoder decoder = new TiffBitmapDecoder(inputStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

                Stream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                TiffBitmapEncoder encoder = new TiffBitmapEncoder();

                int pageCount = decoder.Frames.Count;

                for (int i = 0; i < pageCount; i++)
                {
                    if (pageNumbers.Contains(i))
                    {
                        encoder.Frames.Add(decoder.Frames[i]);
                    }
                }

                encoder.Save(outputStream);

                inputStream.Dispose();
                outputStream.Dispose();
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