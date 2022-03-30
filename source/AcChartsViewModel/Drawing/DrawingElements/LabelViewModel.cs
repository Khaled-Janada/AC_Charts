using Infinity.Extensions;
using LiveCharts.Defaults;

namespace AcCharts.Drawing.DrawingElements;

public class LabelViewModel : DrawingElement {

    #region Properties
    public double X { get => Points[0].X; set => Points[0].X = value; }
    public double Y { get => Points[0].Y; set => Points[0].Y = value; }

    public ObservablePoint Point {
        get => Points[0];
        set { (Points.Count == 0).IfElse(() => AddPoint(value), () => Points[0] = value); }
    }
    #endregion

    #region Constructors
    public LabelViewModel(string label) : base(DrawingElementType.Label) {
        Title = label;
        DataLabels = true;
        LabelPoint = _ => Title;
    }
    #endregion

}