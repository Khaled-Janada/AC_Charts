namespace Infinity.Controls;

public class InfToolbarButton : Button {

    #region Label
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
        nameof(Label), typeof(string), typeof(InfToolbarButton), new PropertyMetadata(default(string)));

    public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
    #endregion

    #region Icon
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon), typeof(ImageSource), typeof(InfToolbarButton), new PropertyMetadata(default(ImageSource)));

    public ImageSource Icon { get => (ImageSource)GetValue(IconProperty); set => SetValue(IconProperty, value); }
    #endregion

    #region Constructors
    static InfToolbarButton() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfToolbarButton), new FrameworkPropertyMetadata(typeof(InfToolbarButton)));
    }
    #endregion

}