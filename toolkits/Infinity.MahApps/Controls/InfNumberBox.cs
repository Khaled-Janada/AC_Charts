namespace Infinity.Controls;

public class InfNumberBox : NumericUpDown {

    #region Label
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(InfNumberBox), new PropertyMetadata(default(string)));

    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    #endregion

    #region Constructors
    static InfNumberBox() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfNumberBox), new FrameworkPropertyMetadata(typeof(InfNumberBox)));
    }
    #endregion

}
