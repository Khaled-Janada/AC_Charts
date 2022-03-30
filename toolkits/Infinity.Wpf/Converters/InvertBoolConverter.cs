namespace Infinity.Converters;

public class InvertBoolConverter : ConverterBase<bool, bool> {

    protected override bool Convert(bool inValue) {
        return !inValue;
    }

    protected override bool ConvertBack(bool outValue) {
        return !outValue;
    }

}
