using System.Threading.Tasks;

namespace XFAgeAnalysis.Interfaces
{
    public interface IDataService
    {
        Task<double?> AnalyzePictureAsync(byte[] imagesBytes);
    }
}
