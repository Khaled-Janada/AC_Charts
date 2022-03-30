using System.Collections;
using AcCharts.Drawing.DrawingElements;
using Infinity.Messaging;
using Infinity.Messaging.Messages;
using Infinity.Services;
using LiveCharts;
using LiveCharts.Wpf;

namespace AcCharts.Drawing;

public class Chart : CartesianChart {

    public Chart() {
        Messenger.Default.Register<ScreenShotMessage>(this, message => TakeScreenShot(message, 1), 1);
        Messenger.Default.Register<ScreenShotMessage>(this, message => TakeScreenShot(message, 2), 2);
    }

    #region Overrides
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
        base.OnPropertyChanged(e);

        if (e.Property.Name != nameof(Series) || e.NewValue is not SeriesCollection seriesCollection) return;

        SetStyle(seriesCollection);

        seriesCollection.CollectionChanged += (_, args) => {
            if (args.NewItems is not null) SetStyle(args.NewItems);
        };
    }
    #endregion

    #region Private Methods
    private static void SetStyle(IEnumerable list) {
        foreach (object newItem in list) {
            if (newItem is DrawingElement element) element.Style = Application.Current.Properties["ChartElementStyle"] as Style;
        }
    }

    private void TakeScreenShot(ScreenShotMessage message, int channel) {
        if (channel != 1 || !IsVisible || Window.GetWindow(this)?.IsActive != true || Parent is not FrameworkElement parent) return;
        ScreenShots.TakeScreenShot(parent, message.Element, message.Dpi);
    }
    #endregion

}