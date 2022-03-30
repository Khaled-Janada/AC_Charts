using System.Windows.Documents;

namespace Infinity.Attached;

public class TextBlockExt {

    #region Formatted Text
    public static readonly DependencyProperty FormattedTextProperty = DependencyProperty.RegisterAttached(
        "FormattedText", typeof(IList<Run>), typeof(TextBlockExt),
        new PropertyMetadata(default(IList<Run>), FormattedTextCallback));

    private static void FormattedTextCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not TextBlock textBlock || e.NewValue is not IList<Run> runs) return;
        textBlock.Inlines.AddRange(runs);
    }

    public static void SetFormattedText(DependencyObject element, IList<Run> value) {
        element.SetValue(FormattedTextProperty, value);
    }

    public static IList<Run> GetFormattedText(DependencyObject element) {
        return (IList<Run>)element.GetValue(FormattedTextProperty);
    }
    #endregion

}
