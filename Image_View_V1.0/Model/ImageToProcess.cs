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

        [Ignore]
        public ImageSource ImageSrcThumbnail { get; set; }
        public byte[] ImageSrcThumbnailBytes { get; set; }

        [Ignore]
        public ChunkIHDR ChIHDR { get; set; }
        public string ChIHDRJson { get; set; }

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
