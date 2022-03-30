using System.Collections.ObjectModel;

namespace AcCharts.Drawing;

public class Line : ReadOnlyCollection<Point> {

    #region Properties
    public string Label { get; }
    #endregion

    #region Constructors
    protected Line(string label, IList<Point> list) : base(list) {
        Label = label;
    }

    public Line(string label, params (double x, double y)[] points) : base(points.Select(value => (Point)value).ToList()) {
        Label = label;
    }
    #endregion

}
