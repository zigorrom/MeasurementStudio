using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace DataVisualization.D3DataVisualization
{
    class FormattedTextValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var a = (string)value;
            var par = new Paragraph();
            par.Inlines.Add("dfasfasf");
            par.Inlines.Add(a);
            return par;
//            <Paragraph FontFamily="Palatino Linotype">
//            2<Run Typography.Variants="Superscript">3</Run>
//            14<Run Typography.Variants="Superscript">th</Run>
//            </Paragraph>

            //from string convertion
            //TextRange selection = new TextRange(document.ContentStart, document.ContentEnd);
            //selection.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Superscript);
            //throw new NotImplementedException();
            //return value;
        }
    }
}
