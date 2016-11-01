using System;
using System.Globalization;
using System.Windows.Data;

namespace laps_2_1
{
    public class CatImgResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (CatType) value;
            switch (t)
            {
                case CatType.Cat1:
                    return Cat1;
                case CatType.Cat2:
                    return Cat2;
                case CatType.Cat3:
                    return Cat3;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Cat1 { get; set; }

        public object Cat2 { get; set; }

        public object Cat3 { get; set; }
    }
}