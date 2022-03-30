namespace Infinity.Controls;

public class InfToolbarTab : ItemsControl {

    #region Header
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached(
        nameof(Header), typeof(string), typeof(InfToolbarTab), new PropertyMetadata(default(string)));

    public string Header { get => (string)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    #endregion

    #region Constructors
    static InfToolbarTab() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfToolbarTab), new FrameworkPropertyMetadata(typeof(InfToolbarTab)));
    }
    #endregion

}