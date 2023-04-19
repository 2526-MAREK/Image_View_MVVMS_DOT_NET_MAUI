namespace Image_View_V1._0.Model
{
    public class ImageToProcess
    {
        public ImageSource ImageSrcMain { get; set; }
        public ImageSource ImageSrcThumbnail { get; set; }
        public ChunkIHDR ChIHDR { get; set; }

        public ImageSource ImageSrcFFT { get; set; }
        public ImageSource ImageSrcMiniature { get; set; }
        public ImageSource ImageSrcHist { get; set; }
        public ImageSource ImageSrcHistR { get; set; }
        public ImageSource ImageSrcHistG { get; set; }
        public ImageSource ImageSrcHistB { get; set; }
    }
}
