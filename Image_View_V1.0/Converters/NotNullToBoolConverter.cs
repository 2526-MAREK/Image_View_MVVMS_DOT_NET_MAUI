/**
* @file NotNullToBoolConverter.cs
* @brief Contains the NotNullToBoolConverter class, which is used to convert a chunk to a boolean value.
*/

using System.Globalization;

namespace Image_View_V1._0.Converters
{
    public class NotNullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChunkIHDR chIHDR)
            {
                return chIHDR != null;
            }

            if (value is ChunkgAMA chgAMA)
            {
                return chgAMA != null;
            }

            if (value is ChunkhIST chhIST)
            {
                return chhIST != null;
            }

            if (value is ChunkiTXt chiTXt)
            {
                return chiTXt != null;
            }

            if (value is ChunkoFFs choFFs)
            {
                return choFFs != null;
            }

            if (value is ChunkpHYs chpHYs)
            {
                return chpHYs != null;
            }

            if (value is ChunksBIT chsBIT)
            {
                return chsBIT != null;
            }

            if (value is ChunksPLT chsPLT)
            {
                return chsPLT != null;
            }

            if (value is ChunksRGB chsRGB)
            {
                return chsRGB != null;
            }

            if (value is ChunksRGB chtEXt)
            {
                return chtEXt != null;
            }

            if (value is ChunktIME chtIME)
            {
                return chtIME != null;
            }

            return false;
        }

   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
