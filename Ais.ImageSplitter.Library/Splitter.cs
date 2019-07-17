using System;
using System.Threading.Tasks;

namespace Ais.ImageSplitter.Library
{
    public class Splitter : ISplitter
    {
        public async Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pages)
        {
            var result = new SplitResult {InputFilePath = inputFilePath};

            try
            {
                // todo add splitting logic here
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