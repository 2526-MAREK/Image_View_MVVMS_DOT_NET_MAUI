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

        public string SerializeChunkIHDR(ChunkIHDR chunkIHDR)
        {
            return JsonConvert.SerializeObject(chunkIHDR);
        }

        public ChunkIHDR DeserializeChunkIHDR(string json)
        {
            return JsonConvert.DeserializeObject<ChunkIHDR>(json);
        }
    }
}
