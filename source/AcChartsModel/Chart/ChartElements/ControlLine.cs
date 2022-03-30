using AcCharts.Drawing;

namespace AcCharts.Chart.ChartElements;

public class ControlLine : Line {

    public ControlLineType ControlLineType { get; }

    public ControlLine(ControlLineType type, IList<Point> list) : base(type.ToString().ToUpper(), list) {
        ControlLineType = type;
    }

}
