using AcCharts.Drawing.DrawingElements;
using Infinity.Extensions;
using LiveCharts.Defaults;

namespace AcCharts.Chart.ChartElements;

public class ControlLineViewModel : LineViewModel {

    #region Static
    private static DrawingElementType ToElementType(ControlLineType controlLineType) {
        return controlLineType switch {
            ControlLineType.Acl => DrawingElementType.CentralControlLine,
            ControlLineType.Alcl => DrawingElementType.LowerControlLine,
            ControlLineType.Aucl => DrawingElementType.UpperControlLine,
            _                   => throw new ArgumentOutOfRangeException()
        };
    }
    #endregion

    #region Properties
    private ControlLine ControlLine { get; set; }
    #endregion

    #region Constructors
    public ControlLineViewModel(ControlLine controlLine) : base(ToElementType(controlLine.ControlLineType), controlLine.Label) {
        ControlLine = controlLine;
        AddPoints(controlLine.Select(GetPoint).ToList());
    }
    #endregion

    #region Public Methods
    public void SetYMax(double padding) {
        LineLabelPoint = new ObservablePoint(Points[^1].X, Points[^1].Y * padding);
    }

    public void SetXMax(double xMax, double padding) {
        if (DrawingElementType != DrawingElementType.UpperControlLine) return;

        var points = ControlLine.Select(GetPoint).ToList();
        Points.Clear();

        if (xMax > ControlLine[^1].X) {
            AddPoints(ControlLine.Select(GetPoint).ToList());
            LineLabelPoint = Points[^1];
            return;
        }

        var nLimit = 0;

        for (int n = 0; n < points.Count; n++) {
            if (points[n].X > xMax) {
                nLimit = n;
                break;
            }

            Points.Add(points[n]);
        }

        var m = (points[nLimit].Y - points[nLimit - 1].Y) / (points[nLimit].X - points[nLimit - 1].X);
        var yLimit = m * (xMax - points[nLimit - 1].X) + points[nLimit - 1].Y;


        var limitPoint = new ObservablePoint(xMax, yLimit);
        Points.Add(limitPoint);
        LineLabelPoint = limitPoint;

        SetYMax(padding);
    }
    #endregion

}

public class ControlLines : ReadOnlyCollection<ControlLineViewModel> {

    public ControlLines(ChartSpecs chartSpecs) : base(GetControlLines(chartSpecs)) { }

    #region Static
    private static IList<ControlLineViewModel> GetControlLines(ChartSpecs chartSpecs) {
        return new List<ControlLineViewModel> { new(chartSpecs.Lcl), new(chartSpecs.Ccl), new(chartSpecs.Ucl) };
    }
    #endregion

    #region Private Properties
    private double _padding;
    #endregion

    #region Public Methods
    public void SetXMax(double xMax) {
        this.ForEach(line => line.SetXMax(xMax, _padding));
    }

    public void SetYMax(double padding) {
        _padding = padding;
        this.ForEach(line => line.SetYMax(_padding));
    }
    #endregion

}
