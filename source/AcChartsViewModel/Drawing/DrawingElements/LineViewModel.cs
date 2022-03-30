using LiveCharts.Defaults;

namespace AcCharts.Drawing.DrawingElements;

public class LineViewModel : DrawingElement {

    #region Public Properties
    public LabelViewModel Label { get; }
    public ObservablePoint LineLabelPoint { get => Label.Point; set => Label.Point = value; }
    #endregion

    protected LineViewModel(DrawingElementType drawingElementType, string label) : base(drawingElementType) {
        Label = new LabelViewModel(label);
    }

}