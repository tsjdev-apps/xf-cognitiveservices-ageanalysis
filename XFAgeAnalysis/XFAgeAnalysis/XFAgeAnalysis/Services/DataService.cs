using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XFAgeAnalysis.Common;
using XFAgeAnalysis.Interfaces;

namespace XFAgeAnalysis.Services
{
    public class DataService : IDataService
    {
        public async Task<double?> AnalyzePictureAsync(byte[] imagesBytes)
        {
            try
            {
                // create faceclient
                var faceClient = new FaceClient(new ApiKeyServiceClientCredentials(Statics.SubscriptionKey)) { Endpoint = Statics.Endpoint };

                // detect face and analyze age
                var imageStream = new MemoryStream(imagesBytes);
                var faceAttributes = new List<FaceAttributeType> { FaceAttributeType.Age };
                var detectedFaces = await faceClient.Face.DetectWithStreamAsync(imageStream, returnFaceAttributes: faceAttributes);

                if (detectedFaces == null || detectedFaces.Count == 0)
                    return null;

                return detectedFaces.First().FaceAttributes.Age;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(AnalyzePictureAsync)} | {ex}");
            }

            return null;
        }
    }
}
