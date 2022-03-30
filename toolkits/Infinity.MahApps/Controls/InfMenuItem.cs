namespace Infinity.Controls;

public class InfMenuItem : MenuItem {

    #region Item Icon
    public static readonly DependencyProperty ItemIconProperty = DependencyProperty.RegisterAttached(
        nameof(ItemIcon), typeof(ImageSource), typeof(InfMenuItem),
        new PropertyMetadata(default(ImageSource?), PropertyChangedCallback));

    public ImageSource? ItemIcon { get => (ImageSource?)GetValue(ItemIconProperty); set => SetValue(ItemIconProperty, value); }
    #endregion

    static InfMenuItem() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(InfMenuItem), new FrameworkPropertyMetadata(typeof(InfMenuItem)));
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not InfMenuItem menuItem) return;

        switch (e.Property.Name) {
            case nameof(ItemIcon):
                double size = menuItem.FontSize * 1.5;
                Image image = new() { Source = menuItem.ItemIcon, Width = size, Height = size };
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.Fant);
                menuItem.Icon = image;
                break;
        }
    }

}