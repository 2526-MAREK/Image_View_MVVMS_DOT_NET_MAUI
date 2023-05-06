/**
* @file ImageToProcess.cs
* @brief Contains the ImageToProcess class, which represents an image with its various properties and related data.
*/

using SQLite;

namespace Image_View_V1._0.Model
{
    /**
    * @class ImageToProcess
    * @brief Represents an image with its various properties and related data.
    */
    public class ImageToProcess
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  ///< @brief Unique identifier for the image.
        public string NameOfImageToDataBase { get; set; }   ///< @brief Name of the image in the database.

        [Ignore]
        public ImageSource ImageSrcMain { get; set; }   ///< @brief ImageSource object representing the main image.
        public byte[] ImageSrcMainBytes { get; set; }   ///< @brief Byte array representing the main image.

        /*[Ignore]
        public ImageSource ImageSrcThumbnail { get; set; }
        public byte[] ImageSrcThumbnailBytes { get; set; }*/

        [Ignore]
        public ChunkIHDR ChIHDR { get; set; }   ///< @brief ChunkIHDR object representing the IHDR chunk.
        public string ChIHDRJson { get; set; }  ///< @brief JSON string representing the IHDR chunk.

        [Ignore]
        public ChunkgAMA ChgAMA { get; set; }   ///< @brief ChunkgAMA object representing the gAMA chunk.
        public string ChgAMAJson { get; set; }  ///< @brief JSON string representing the gAMA chunk.

        [Ignore]
        public ChunkhIST ChhIST { get; set; }   ///< @brief ChunkhIST object representing the hIST chunk.
        public string ChhISTJson { get; set; }  ///< @brief JSON string representing the hIST chunk.

        [Ignore]
        public ChunkiTXt ChiTXt { get; set; }   ///< @brief ChunkiTXt object representing the iTXt chunk.
        public string ChiTXtJson { get; set; }  ///< @brief JSON string representing the iTXt chunk.

        [Ignore]
        public ChunkoFFs ChoFFs { get; set; }   ///< @brief ChunkoFFs object representing the oFFs chunk.
        public string ChoFFsJson { get; set; }  ///< @brief JSON string representing the oFFs chunk.

        [Ignore]
        public ChunkpHYs ChpHYs { get; set; }   ///< @brief ChunkpHYs object representing the pHYs chunk.
        public string ChpHYsJson { get; set; }  ///< @brief JSON string representing the pHYs chunk.

        [Ignore]
        public ChunksBIT ChsBIT { get; set; }   ///< @brief ChunksBIT object representing the sBIT chunk.
        public string ChsBITJson { get; set; }  ///< @brief JSON string representing the sBIT chunk.

        [Ignore]
        public ChunksPLT ChsPLT { get; set; }   ///< @brief ChunksPLT object representing the sPLT chunk.
        public string ChsPLTJson { get; set; }  ///< @brief JSON string representing the sPLT chunk.

        [Ignore]
        public ChunksRGB ChsRGB { get; set; }   ///< @brief ChunksRGB object representing the sRGB chunk.
        public string ChsRGBJson { get; set; }  ///< @brief JSON string representing the sRGB chunk.

        [Ignore]
        public ChunktEXt ChtEXt { get; set; }   ///< @brief ChunktEXt object representing the tEXt chunk.
        public string ChtEXtJson { get; set; }  ///< @brief JSON string representing the tEXt chunk.

        [Ignore]
        public ChunktIME ChtIME { get; set; }   ///< @brief ChunktIME object representing the tIME chunk.
        public string ChtIMEJson { get; set; }  ///< @brief JSON string representing the tIME chunk.

        [Ignore]
        public ChunksTER ChsTER { get; set; }   ///< @brief ChunksTER object representing the tER chunk.
        public string ChsTERJson { get; set; }  ///< @brief JSON string representing the tER chunk.

        [Ignore]
        public ImageSource ImageSrcFFT { get; set; }    ///< @brief ImageSource object representing the FFT image.
        public byte[] ImageSrcFFTBytes { get; set; }    ///< @brief Byte array representing the FFT image.

        [Ignore]
        public ImageSource ImageSrcMiniature { get; set; }  ///< @brief ImageSource object representing the miniature image.
        public byte[] ImageSrcMiniatureBytes { get; set; }  ///< @brief Byte array representing the miniature image.

        [Ignore]
        public ImageSource ImageSrcHist { get; set; }   ///< @brief ImageSource object representing the histogram image.
        public byte[] ImageSrcHistBytes { get; set; }   ///< @brief Byte array representing the histogram image.
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
