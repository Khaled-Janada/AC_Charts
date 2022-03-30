namespace Infinity.Controls;

public class InfToolbar : ItemsControl {

    #region Shadow Color
    public static readonly DependencyProperty ShadowColorProperty = DependencyProperty.RegisterAttached(
        nameof(ShadowColor), typeof(Color), typeof(InfToolbar), new PropertyMetadata(default(Color)));

    public Color ShadowColor { get => (Color)GetValue(ShadowColorProperty); set => SetValue(ShadowColorProperty, value); }
    #endregion

    #region Shadow Depth
    public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached(
        nameof(ShadowDepth), typeof(double), typeof(InfToolbar), new PropertyMetadata(default(double)));

    public double ShadowDepth { get => (double)GetValue(ShadowDepthProperty); set => SetValue(ShadowDepthProperty, value); }
    #endregion

    #region Constructors
    static InfToolbar() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfToolbar), new FrameworkPropertyMetadata(typeof(InfToolbar)));
    }
    #endregion

}