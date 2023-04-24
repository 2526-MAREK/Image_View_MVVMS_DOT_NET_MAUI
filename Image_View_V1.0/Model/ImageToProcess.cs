using SQLite;

namespace Image_View_V1._0.Model
{
    public class ImageToProcess
    {
        public int Id { get; set; }
        public string NameOfImageToDataBase { get; set; }
        public ImageSource ImageSrcMain { get; set; }
        public ImageSource ImageSrcThumbnail { get; set; }
        public ChunkIHDR ChIHDR { get; set; }

        public ImageSource ImageSrcFFT { get; set; }
        public ImageSource ImageSrcMiniature { get; set; }
        public ImageSource ImageSrcHist { get; set; }
    }
}
