using System.Windows.Input;

namespace Infinity.Controls;

public class InfWindow : MetroWindow {

    #region Title
    public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
        nameof(TitleFontFamily), typeof(FontFamily), typeof(InfWindow), new PropertyMetadata(default(FontFamily)));

    public FontFamily TitleFontFamily {
        get => (FontFamily)GetValue(TitleFontFamilyProperty);
        set => SetValue(TitleFontFamilyProperty, value);
    }

    public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
        nameof(TitleFontSize), typeof(double), typeof(InfWindow), new PropertyMetadata(default(double)));

    public double TitleFontSize { get => (double)GetValue(TitleFontSizeProperty); set => SetValue(TitleFontSizeProperty, value); }

    public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(
        nameof(TitleFontWeight), typeof(FontWeight), typeof(InfWindow), new PropertyMetadata(default(FontWeight)));

    public FontWeight TitleFontWeight {
        get => (FontWeight)GetValue(TitleFontWeightProperty);
        set => SetValue(TitleFontWeightProperty, value);
    }
    #endregion

    #region Dialog Result
    public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
        nameof(DialogResult), typeof(bool?), typeof(InfWindow), new PropertyMetadata(default(bool?), ResulChangedCallback));

    public new bool? DialogResult { get => (bool?)GetValue(DialogResultProperty); set => SetValue(DialogResultProperty, value); }

    private static void ResulChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not InfWindow window) return;
        (window as Window).DialogResult = (bool?)e.NewValue;
    }
    #endregion
    
    #region Constructors
    static InfWindow() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfWindow), new FrameworkPropertyMetadata(typeof(InfWindow)));
    }

    public InfWindow() {
        WindowStartupLocation = (Owner is null) ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
    }
    #endregion

    #region Overrides
    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        if (GetTemplateChild("PART_Icon") is not ContentControl content) return;
        content.Margin = new Thickness(3);
    }
    #endregion

    #region Protected Methods
    protected void CloseWindow(object sender, ExecutedRoutedEventArgs e) => Close();
    #endregion

}
