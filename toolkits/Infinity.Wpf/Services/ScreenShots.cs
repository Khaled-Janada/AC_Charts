using System.IO;
using System.Windows.Media.Imaging;

namespace Infinity.Services;

public static class ScreenShots {

    public static void TakeScreenShot(FrameworkElement element, string name = "Screenshot", int dpi = 300) {
        var dpiScale = VisualTreeHelper.GetDpi(element);

        var window = element as Window;
        var size = element.RenderSize;

        var actualWidth = (window?.WindowState  == WindowState.Maximized) ? SystemParameters.WorkArea.Width : size.Width;
        var actualHeight = (window?.WindowState == WindowState.Maximized) ? SystemParameters.WorkArea.Height : size.Height;

        int width = (int)(actualWidth   * dpi * dpiScale.DpiScaleX / dpiScale.PixelsPerInchX);
        int height = (int)(actualHeight * dpi * dpiScale.DpiScaleY / dpiScale.PixelsPerInchY);

        RenderTargetBitmap renderTargetBitmap = new(width, height, dpi, dpi, PixelFormats.Pbgra32);

        renderTargetBitmap.Render(element);
        PngBitmapEncoder jpegImage = new();
        jpegImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

        var dlg = new SaveFileDialog {
            FileName = name, DefaultExt = ".png", Filter = "PNG Images (.png)|*.png", Title = "Save Screenshot"
        };

        var currentWindow = window ?? Window.GetWindow(element) ?? Application.Current.MainWindow;
        if (dlg.ShowDialog(currentWindow) != true) return;

        using Stream fileStream = File.Create(dlg.FileName);
        jpegImage.Save(fileStream);
    }

}
