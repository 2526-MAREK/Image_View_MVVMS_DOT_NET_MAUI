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
        public async Task<byte[]> ImageSourceToByteArrayAsync(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
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
