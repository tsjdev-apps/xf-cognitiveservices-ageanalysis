using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using XFAgeAnalysis.Interfaces;

namespace XFAgeAnalysis.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task<byte[]> PickPictureAsync()
        {
            // initialie
            await CrossMedia.Current.Initialize();

            // pick photo
            var mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium, CompressionQuality = 90 });
            if (mediaFile == null)
                return null;

            // return bytes
            return GetImageBytes(mediaFile.GetStream());
        }

        public async Task<byte[]> TakePictureAsync()
        {
            // initialize
            await CrossMedia.Current.Initialize();

            // check if camera is available
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return null;

            // take photo
            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { Directory = "AnalyzeImages", Name = $"{Guid.NewGuid()}.jpg" });
            if (mediaFile == null)
                return null;

            // return bytes
            return GetImageBytes(mediaFile.GetStream());
        }


        private byte[] GetImageBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);

                return ms.ToArray();
            }
        }
    }
}
