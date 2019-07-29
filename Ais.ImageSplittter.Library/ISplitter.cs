using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ais.ImageSplittter.Library
{
    interface ISplitter
    {
        Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pageNumbers);
    }
}
