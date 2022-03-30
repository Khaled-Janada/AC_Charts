using System.Windows;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace AcCharts.Drawing.DrawingElements;

public class DrawingElement : LineSeries {

    #region Element Type
    public static readonly DependencyProperty DrawingElementTypeProperty = DependencyProperty.Register(
        nameof(DrawingElementType), typeof(DrawingElementType), typeof(DrawingElement),
        new PropertyMetadata(default(DrawingElementType)));

    public DrawingElementType DrawingElementType {
        get => (DrawingElementType)GetValue(DrawingElementTypeProperty);
        init => SetValue(DrawingElementTypeProperty, value);
    }
    #endregion

    #region Properties
    public ChartValues<ObservablePoint> Points { get; }
    #endregion

    #region Constructors
    internal DrawingElement(DrawingElementType drawingElementType) {
        DrawingElementType = drawingElementType;
        Points = new ChartValues<ObservablePoint>();
        Values = Points;
        LineSmoothness = 0;
    }
    #endregion

    #region Public Methods
    public void AddPoint(ObservablePoint point) => Points.Add(point);
    public void AddPoints(IEnumerable<ObservablePoint> points) => Points.AddRange(points);
    #endregion

    #region Static
    protected static ObservablePoint GetPoint(Point point) {
        return new ObservablePoint(point.X, point.Y);
    }
    #endregion

}
