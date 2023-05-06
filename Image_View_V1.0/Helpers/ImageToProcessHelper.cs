/**
* @file ImageToProcessHelper.cs
* @brief Contains the ImageToProcessHelper class, which is used to convert images to byte arrays and vice versa.
*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_View_V1._0.Helpers
{
    public class ImageToProcessHelper
    {
        private async Task<StreamImageSource> ConvertFileImageSourceToFileStreamImageSource(FileImageSource fileImageSource)
        {
            if (fileImageSource == null)
            {
                return null;
            }

            var filePath = fileImageSource.File;
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            StreamImageSource streamImageSource = new StreamImageSource
            {
                Stream = token => Task.FromResult<Stream>(File.OpenRead(filePath))
            };

            return streamImageSource;
        }
        public async Task<byte[]> ImageSourceToByteArrayAsync(ImageSource imageSource)
        {
            StreamImageSource streamImageSource;

            if (imageSource is FileImageSource fileImageSource)
            {
                streamImageSource = await ConvertFileImageSourceToFileStreamImageSource(fileImageSource);
            }
            else if (imageSource is StreamImageSource)
            {
                streamImageSource = (StreamImageSource)imageSource;
            }
            else
            {
                // Handle other cases or throw an exception
                throw new NotSupportedException("Unsupported ImageSource type.");
            }

            Stream stream = await streamImageSource.Stream(CancellationToken.None);
            MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public ImageSource ByteArrayToImageSource(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null;
            }
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        public T DeserializeChunk<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
            public string SerializeChunk<T>(T chunk)
        {
            return JsonConvert.SerializeObject(chunk);
        }

        public async Task<(T chunk, string json)> LoadChunkData<T>(Func<Task<T>> getChunkMethod)
        {
            T chunk = await getChunkMethod();
            string json = SerializeChunk(chunk);
            return (chunk, json);
        }
    }
}
