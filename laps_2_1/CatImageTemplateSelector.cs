using System.Windows;
using System.Windows.Controls;

namespace laps_2_1
{
    public class CatImageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Cat1 { get; set; }
        public DataTemplate Cat2 { get; set; }
        public DataTemplate Cat3 { get; set; }
    }
}