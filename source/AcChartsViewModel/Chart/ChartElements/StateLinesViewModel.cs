using AcCharts.Drawing;
using AcCharts.Drawing.DrawingElements;
using Infinity.Extensions;
using LiveCharts.Defaults;

namespace AcCharts.Chart.ChartElements;

public class StateLineViewModelViewModel : LineViewModel {

    #region Constructors
    public StateLineViewModelViewModel(Line stateLine) : base(DrawingElementType.StateLine, stateLine.Label) {
        var p1 = new ObservablePoint(stateLine[0].X, stateLine[0].Y);
        var p2 = new ObservablePoint(stateLine[0].X, stateLine[0].Y) { X = 0 };
        AddPoints(new[] { p1, p2 });

        LineLabelPoint = p2;
    }
    #endregion

    #region Public Methods
    public void SetXMax(double xMax) {
        Points[1].X = xMax;
    }
    #endregion

}

public class StateLines : ReadOnlyCollection<StateLineViewModelViewModel> {

    #region Static
    private static IList<StateLineViewModelViewModel> GetStateLines(IEnumerable<Line> stateLines) {
        return stateLines.Select(line => new StateLineViewModelViewModel(line)).ToList();
    }
    #endregion

    public StateLines(IEnumerable<Line> stateLines) : base(GetStateLines(stateLines)) { }

    #region Public Methods
    public void SetXMax(double xMax) {
        Items.ForEach(line => line.SetXMax(xMax));
    }
    #endregion

}