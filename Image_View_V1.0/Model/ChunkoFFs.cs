/**
* @file ChunkoFFs.cs
* @brief Contains the ChunkoFFs class, which represents the oFFs chunk.
*/

namespace Image_View_V1._0.Model
{ 
    public class ChunkoFFs
    {
        public string PaletteName { get; set; }
        public int SampleDepth { get; set; }

       public List<ChunkEntriesType> Entries { get; set; }
    }
}
