using System;
using System.Globalization;
using System.Windows.Data;

namespace laps_2_1
{
    public class CatShopItemConverterImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ClewShopItem)
                return Clew;
            if (value is ShieldShopItem)
                return Shield;
            throw new Exception();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Clew { get; set; }

        public object Shield { get; set; }
    }
}