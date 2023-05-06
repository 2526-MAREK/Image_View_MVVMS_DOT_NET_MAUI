/**
* @file ChunkiTXt.cs
* @brief Contains the ChunkiTXt class, which represents the iTXt chunk.
*/

namespace Image_View_V1._0.Model
{
    public class ChunkiTXt
    {
        public string Keyword { get; set; }
        public int CompressionFlag { get; set; }
        public int CompressionMethod { get; set; }

        public string LanguageTag { get; set; }
        public string TranslatedKeyword { get; set; }
        public string Text { get; set; }
    }
}
