using System;
using System.Collections.Generic;
using System.Text;

namespace Ais.ImageSplittter.Library
{
    public class SplitResult
    {
        public string InputFilePath { get; set; }
        public string ErrorStatus { get; set; }
        public string StackTrace { get; set; }
    }
}
