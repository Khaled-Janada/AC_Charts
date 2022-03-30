using LiveCharts.Defaults;

namespace AcCharts.Drawing.DrawingElements;

public class SmoothLine : LineViewModel {

    public SmoothLine(string label) : base(DrawingElementType.SmoothLine, label) { }

    #region Public Properties
    public void Update(IList<ObservablePoint> points) {
        Points.Clear();
        AddPoints(points);
        LineLabelPoint = points[^1];
    }
    #endregion

}