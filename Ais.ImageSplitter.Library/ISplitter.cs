using System.Threading.Tasks;

namespace Ais.ImageSplitter.Library
{
    public interface ISplitter
    {
        Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pages);
    }
}