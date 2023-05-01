using SQLite;

namespace Image_View_V1._0.Model
{
    public class ImageToProcess
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameOfImageToDataBase { get; set; }

        [Ignore]
        public ImageSource ImageSrcMain { get; set; }
        public byte[] ImageSrcMainBytes { get; set; }

        /*[Ignore]
        public ImageSource ImageSrcThumbnail { get; set; }
        public byte[] ImageSrcThumbnailBytes { get; set; }*/

        [Ignore]
        public ChunkIHDR ChIHDR { get; set; }
        public string ChIHDRJson { get; set; }

        [Ignore]
        public ChunkgAMA ChgAMA { get; set; }
        public string ChgAMAJson { get; set; }

        [Ignore]
        public ChunkhIST ChhIST { get; set; }
        public string ChhISTJson { get; set; }

        [Ignore]
        public ChunkiTXt ChiTXt { get; set; }
        public string ChiTXtJson { get; set; }

        [Ignore]
        public ChunkoFFs ChoFFs { get; set; }
        public string ChoFFsJson { get; set; }

        [Ignore]
        public ChunkpHYs ChpHYs { get; set; }
        public string ChpHYsJson { get; set; }

        [Ignore]
        public ChunksBIT ChsBIT { get; set; }
        public string ChsBITJson { get; set; }

        [Ignore]
        public ChunksPLT ChsPLT { get; set; }
        public string ChsPLTJson { get; set; }

        [Ignore]
        public ChunksRGB ChsRGB { get; set; }
        public string ChsRGBJson { get; set; }

        [Ignore]
        public ChunktEXt ChtEXt { get; set; }
        public string ChtEXtJson { get; set; }

        [Ignore]
        public ChunktIME ChtIME { get; set; }
        public string ChtIMEJson { get; set; }

        [Ignore]
        public ChunksTER ChsTER { get; set; }
        public string ChsTERJson { get; set; }

        [Ignore]
        public ImageSource ImageSrcFFT { get; set; }
        public byte[] ImageSrcFFTBytes { get; set; }

        [Ignore]
        public ImageSource ImageSrcMiniature { get; set; }
        public byte[] ImageSrcMiniatureBytes { get; set; }

        [Ignore]
        public ImageSource ImageSrcHist { get; set; }
        public byte[] ImageSrcHistBytes { get; set; }
        /*[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NameOfImageToDataBase { get; set; }
        public ImageSource ImageSrcMain { get; set; }
        public ImageSource ImageSrcThumbnail { get; set; }
        public ChunkIHDR ChIHDR { get; set; }

        public ImageSource ImageSrcFFT { get; set; }
        public ImageSource ImageSrcMiniature { get; set; }
        public ImageSource ImageSrcHist { get; set; }*/
    }
}
