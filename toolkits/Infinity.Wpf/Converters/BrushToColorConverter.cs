namespace Infinity.Converters;

[ValueConversion(typeof(SolidColorBrush), typeof(Color))]
public class BrushToColorConverter : ConverterBase<SolidColorBrush, Color> {

    protected override Color Convert(SolidColorBrush inValue) {
        return inValue.Color;
    }

    protected override SolidColorBrush ConvertBack(Color outValue) {
        return new(outValue);
    }

}
