namespace Infinity.Controls; 

public class InfTextBox : TextBox {

    #region Label
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(InfTextBox), new PropertyMetadata(default(string)));

    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    #endregion

    #region Constructors
    static InfTextBox() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfTextBox), new FrameworkPropertyMetadata(typeof(InfTextBox)));
    }
    #endregion

}
