using System.Data;
using System.Globalization;

namespace Infinity.Converters;

// ConverterBase without parameters
public abstract class ConverterBase<TIn, TOut> : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return Convert((TIn)value) ?? throw new NoNullAllowedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return ConvertBack((TOut)value) ?? throw new NoNullAllowedException();
    }

    protected abstract TOut Convert(TIn inValue);
    protected abstract TIn ConvertBack(TOut outValue);

}
