/**
* @file ChunksPLT.cs
* @brief Contains the ChunksPLT class, which represents the PLTE chunk.
*/

namespace Image_View_V1._0.Model
{ 
    public class ChunksPLT
    {
        public string PaletteName { get; set; }
        public int SampleDepth { get; set; }

        public List<ChunkEntriesType> Entries { get; set; }

    }
}
