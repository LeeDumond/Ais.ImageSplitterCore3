using System.Threading.Tasks;

namespace Ais.ImageSplitter.Wpf
{
    public interface ISplitter
    {
        Task<SplitResult> SplitAsync(string inputFilePath, string outputFilePath, int[] pageNumbers);
    }
}