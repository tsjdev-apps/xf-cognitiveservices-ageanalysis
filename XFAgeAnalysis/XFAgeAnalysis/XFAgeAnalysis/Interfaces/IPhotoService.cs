using System.Threading.Tasks;

namespace XFAgeAnalysis.Interfaces
{
    public interface IPhotoService
    {
        Task<byte[]> PickPictureAsync();

        Task<byte[]> TakePictureAsync();
    }
}
