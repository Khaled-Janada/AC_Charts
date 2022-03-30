using AcCharts.Drawing.DrawingElements;
using Infinity.Events;
using LiveCharts.Defaults;

namespace AcCharts.Chart.ChartElements;

public class ObservationViewModel : DrawingElement {

    #region Private Properties
    private Observation Observation { get; }
    #endregion

    #region Public Properties
    public double TimeToFail { get => Observation.TimeToFail; set => SetTimeToFail(value); }
    public int State { get => Observation.StateNumber; set => SetState(value); }
    #endregion

    #region Events
    public event ValueChangedEventHandler<double>? TimeChanged;
    #endregion

    public ObservationViewModel(Observation observation) : base(DrawingElementType.Observation) {
        Observation = observation;
        Points.Add(new ObservablePoint(observation.TimeToFail, observation.Median));

        Title = observation.Label;
        LabelPoint = _ => Title;
    }

    #region Private Properties
    private void SetState(int stateNumber) {
        Observation.StateNumber = stateNumber;
        Points[0].Y = Observation.Median;
    }

    private void SetTimeToFail(double timeToFail) {
        Observation.TimeToFail = timeToFail;
        Points[0].X = timeToFail;
        TimeChanged?.Invoke(this, new ValueChangedEventArgs<double>(timeToFail));
    }
    #endregion

}